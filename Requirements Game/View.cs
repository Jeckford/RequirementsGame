using Requirements_Game;
using System.Windows.Forms;

public class View : Panel {

    protected CustomTableLayoutPanel ViewTableLayoutPanel;

    public View() {

        // View Properties

        this.Dock = DockStyle.Fill;
        this.AutoScroll = true;

        // ViewTableLayoutPanel

        ViewTableLayoutPanel = new CustomTableLayoutPanel();
        
        this.Controls.Add(ViewTableLayoutPanel);

    }

    protected void AutoResizeViewTableLayoutPanel() {
      
        int fullWidth = 0;
        int[] columnWidths = ViewTableLayoutPanel.GetColumnWidths();
        
        foreach (int width in columnWidths) {

            fullWidth += width;

        }

        ViewTableLayoutPanel.Width = fullWidth;

        int fullHeight = 0;
        int[] rowHeights = ViewTableLayoutPanel.GetRowHeights();

        foreach (int height in rowHeights) {

            fullHeight += height;

        }

        ViewTableLayoutPanel.Height = fullHeight;

    }

}
