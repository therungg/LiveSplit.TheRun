using System;
using System.Drawing;
using System.Windows.Forms;

namespace LiveSplit.UI.Components;

public class UploadToast : Form
{
    private readonly Label label;
    private readonly Timer closeTimer;

    private readonly Control owner;

    public UploadToast(Control owner)
    {
        this.owner = owner;

        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.Manual;
        ShowInTaskbar = false;
        TopMost = true;
        BackColor = Color.FromArgb(30, 30, 30);
        ForeColor = Color.White;
        Size = new Size(250, 36);
        Opacity = 0.9;
        Padding = new Padding(8, 0, 8, 0);

        label = new Label
        {
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft,
            Font = new Font("Segoe UI", 9F),
            ForeColor = Color.White,
            AutoEllipsis = true
        };
        Controls.Add(label);

        closeTimer = new Timer { Interval = 2000 };
        closeTimer.Tick += (s, e) =>
        {
            closeTimer.Stop();
            Close();
        };

        PositionBelowOwner();
    }

    private void PositionBelowOwner()
    {
        var bounds = owner.Bounds;
        int x = bounds.Left + (bounds.Width - Width) / 2;
        int y = bounds.Bottom;
        Location = new Point(x, y);
    }

    public void ShowUploading()
    {
        closeTimer.Stop();
        PositionBelowOwner();
        label.Text = "therun.gg: Syncing stats...";
        label.ForeColor = Color.White;
        Show();
    }

    public void ShowSuccess()
    {
        label.Text = "therun.gg: Stats synced!";
        label.ForeColor = Color.FromArgb(100, 220, 100);
        closeTimer.Start();
    }

    public void ShowError()
    {
        label.Text = "therun.gg: Sync failed.";
        label.ForeColor = Color.FromArgb(255, 100, 100);
        closeTimer.Start();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            closeTimer?.Dispose();
            label?.Dispose();
        }
        base.Dispose(disposing);
    }

    protected override CreateParams CreateParams
    {
        get
        {
            var cp = base.CreateParams;
            cp.ExStyle |= 0x00000080; // WS_EX_TOOLWINDOW - hide from Alt+Tab
            return cp;
        }
    }
}
