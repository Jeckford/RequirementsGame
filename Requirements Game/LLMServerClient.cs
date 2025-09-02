using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

public class LLMServerClient {

    private static LLMServerClient ServerClient;

    static LLMServerClient() {

        ServerClient = new LLMServerClient();

    }

    public static async void SendMessage(String Question) {

        string result = await ServerClient.Chat(Question);

        GlobalVariables.ChatReply = result;

    }

    //

    private Process llamaProcess;
    private StringBuilder conversationHistory;

    public LLMServerClient() : this(6, 1024) {
    }

    public LLMServerClient(int threads, int tokenSize) {

        // Initialise conversation

        conversationHistory = new StringBuilder("<|system|> You are a helpful assistant ");

        // Get model based on available RAM

        double totalRam = GetTotalMemory();
        double availableRamMB = GetAvailableMemory();
        string modelPath = "";

        if (availableRamMB < 4096) { // 4GB

            modelPath = $"{FileSystem.ModelsFolderPath}\\gemma-3-1B-it-QAT-Q4_0.gguf";

        } else if (availableRamMB < 8192) { // 8GB

            modelPath = $"{FileSystem.ModelsFolderPath}\\gemma-3-4b-it-Q4_K_M.gguf";

        } else if (availableRamMB < 16384) { // 16GB

            modelPath = $"{FileSystem.ModelsFolderPath}\\gemma-3-12b-it-Q4_K_M.gguf";

        } else { // 16GB or more

            modelPath = $"{FileSystem.ModelsFolderPath}\\gemma-3-27B-it-QAT-Q4_0.gguf";

        }

        if (System.IO.File.Exists(modelPath) == false) {

            throw new Exception($"Model '{System.IO.Path.GetFileName(modelPath)}' not found in '{System.IO.Path.GetDirectoryName(modelPath)}'");
        
        }

        // Check if server is already running, exit if so

        var existingProcesses = Process.GetProcessesByName("llama-server");

        if (existingProcesses.Length > 0) {

            llamaProcess = existingProcesses[0];
            return;

        }


        // Start server

        string serverPath = Path.Combine(Application.StartupPath, "llama-b5995-bin-win-cpu-x64", "llama-server.exe");
        string arguments = $"--model \"{modelPath}\" --threads {threads} --ctx-size {tokenSize}";

        var psi = new ProcessStartInfo {

            FileName = serverPath,
            Arguments = arguments,
            UseShellExecute = false,
            CreateNoWindow = true

        };

        llamaProcess = Process.Start(psi);

    }

    public void StopServer() {

        llamaProcess.Kill();
        llamaProcess.Dispose();

    }

    public async Task<string> Chat(string question, Action<string> onPartialResponse = null) {

        conversationHistory.Append($"<|user|> {question} ");

        string url = "http://localhost:8080/completion";

        var jsonBody = new JsonBuilder();
        jsonBody.Items.Add("prompt", $"{conversationHistory} <|assistant|>");
        jsonBody.Items.Add("n_predict", 100);
        jsonBody.Items.Add("temperature", 0.2);
        jsonBody.Items.Add("top_k", 20);
        jsonBody.Items.Add("top_p", 0.8);
        jsonBody.Items.Add("stop", new string[] { "<|user|>", "<|assistant|>", "<|system|>" });
        jsonBody.Items.Add("stream", true);

        var content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");

        try {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var reader = new StreamReader(stream);

            string fullResponse = "";

            while (!reader.EndOfStream) {

                var line = await reader.ReadLineAsync();

                if (!string.IsNullOrWhiteSpace(line)) {

                    try {

                        var json = JsonDocument.Parse(line.Replace("data: ", ""));

                        if (json.RootElement.TryGetProperty("content", out var contentProp)) {

                            string partial = contentProp.GetString();

                            fullResponse += partial;

                            // Chat is written to the visual studio console as its recieved. Eventually this will need to written straight to a form control for display.
                            // I have not implemented that in this visual studio project due to the richtextbox recording the full chat

                            GlobalVariables.ChatReply += partial;

                            onPartialResponse?.Invoke(partial);

                        }

                    } catch (Exception ex) {

                        Debug.Print(ex.Message);

                    }
                }
            }

            conversationHistory.Append($" <|assistant|> {fullResponse} ");

            return fullResponse;

        } catch (Exception ex) {

            return $"Error: {ex.Message}";

        }

    }

    // Original Chat

    public async Task<string> Chat_WaitForEntireResponse(string question) {

        conversationHistory.Append($"<|user|> {question} ");

        string url = "http://localhost:8080/completion";

        var jsonBody = new JsonBuilder();
        jsonBody.Items.Add("prompt", $"{conversationHistory} <|assistant|>");
        jsonBody.Items.Add("n_predict", 100);
        jsonBody.Items.Add("temperature", 0.2);
        jsonBody.Items.Add("top_k", 20);
        jsonBody.Items.Add("top_p", 0.8);
        jsonBody.Items.Add("stop", new string[] { "<|user|>", "<|assistant|>", "<|system|>" });

        var content = new StringContent(jsonBody.ToString(), Encoding.UTF8, "application/json");

        try {

            var client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(responseBody);
            string result = json.RootElement.GetProperty("content").GetString();

            conversationHistory.Append($" <|assistant|> {result} ");

            return result;

        } catch (Exception ex) {

            return $"Error: {ex.Message}";

        }
    }

    private double GetTotalMemory() {

        var searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize FROM Win32_OperatingSystem");

        foreach (var obj in searcher.Get()) {

            return Convert.ToDouble(obj["TotalVisibleMemorySize"]) / 1024;

        }

        return 0;

    }

    private double GetAvailableMemory() {

        var searcher = new ManagementObjectSearcher("SELECT FreePhysicalMemory FROM Win32_OperatingSystem");

        foreach (var obj in searcher.Get()) {

            return Convert.ToDouble(obj["FreePhysicalMemory"]) / 1024;

        }

        return 0;

    }

}