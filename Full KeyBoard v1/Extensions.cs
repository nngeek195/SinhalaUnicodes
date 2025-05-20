using System.Windows.Forms;
using System.Drawing;

public static class ControlExtensions
{
    public static void Opacity(this Control control, float opacity)
    {
        if (opacity < 0.0 || opacity > 1.0)
            throw new ArgumentException("Value must be between 0 and 1.");

        if (control.Parent == null)
        {
            var parent = new Panel
            {
                Dock = DockStyle.None,
                Width = control.Width,
                Height = control.Height,
                Location = control.Location,
                BackColor = Color.Transparent
            };

            control.Parent = parent;
            parent.Controls.Add(control);
            parent.BringToFront();
            control.FindForm().Controls.Add(parent);
        }

        control.Parent.BackColor = Color.FromArgb((int)(255 * (1 - opacity)), 0, 0, 0);
    }
}