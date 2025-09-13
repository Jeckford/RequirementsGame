using System;
using System.Drawing;
using System.Windows.Forms;

class CustomTextButton : CustomLabel {

    private Color _BaseColor;
    private ButtonInteractionEffect _InteractionEffect;

    public CustomTextButton() { 
    
        this.CornerRadius = 12;
        this.TextAlign = ContentAlignment.MiddleCenter;
        this.BackColor = Color.Gray;
        this._InteractionEffect = ButtonInteractionEffect.None;

        this.MouseDown += Me_MouseDown;
        this.MouseEnter += Me_MouseEnter;
        this.MouseUp += Me_MouseUp;
        this.MouseLeave += Me_MouseLeave;

    }

    public ButtonInteractionEffect InteractionEffect {
        
        get => _InteractionEffect;

        set {

            if (!Enum.IsDefined(typeof(ButtonInteractionEffect), value)) {
                
                throw new Exception("Invalid ButtonInteractionEffect value");

            }

            _InteractionEffect = value;

        }
    }

    public new Color BackColor { 
        
        get => _BaseColor;

        set {

            base.BackColor = value;
            _BaseColor = value;
            
        }
    
    }

    // -- Events --

    private void Me_MouseDown(object sender, MouseEventArgs e) {

        if (e.Button == MouseButtons.Left && InteractionEffect != ButtonInteractionEffect.None) {

            if (InteractionEffect == ButtonInteractionEffect.Darken) {

                base.BackColor = ColorManager.DarkenColor(_BaseColor, 0.30);

            } else if (InteractionEffect == ButtonInteractionEffect.Lighten) {

                base.BackColor = ColorManager.LightenColor(_BaseColor, 0.30);

            }

            this.Refresh();

        }

    }

    private void Me_MouseEnter(object sender, EventArgs e) {

        if (InteractionEffect == ButtonInteractionEffect.Darken) {

            base.BackColor = ColorManager.DarkenColor(_BaseColor, 0.15);

        } else if (InteractionEffect == ButtonInteractionEffect.Lighten) {

            base.BackColor = ColorManager.LightenColor(_BaseColor, 0.15);

        }

        this.Refresh();

    }

    private void Me_MouseLeave(object sender, EventArgs e) {

        base.BackColor = _BaseColor;

    }

    private void Me_MouseUp(object sender, MouseEventArgs e) {

        if (InteractionEffect == ButtonInteractionEffect.Darken) {

            base.BackColor = ColorManager.DarkenColor(_BaseColor, 0.15);

        } else if (InteractionEffect == ButtonInteractionEffect.Darken) {

            base.BackColor = ColorManager.LightenColor(_BaseColor, 0.15);

        }

        this.Refresh();

    }

}
