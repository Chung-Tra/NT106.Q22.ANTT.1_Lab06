namespace Client;

/// <summary>
/// Small dialog that asks for an image URL and an optional target size.
/// A width/height of 0 means "keep the original size".
/// </summary>
public partial class InsertImageForm : Form
{
    public string ImageUrl { get; private set; } = string.Empty;
    public int ImgWidth { get; private set; }
    public int ImgHeight { get; private set; }

    public InsertImageForm()
    {
        InitializeComponent();
        btnOk.Click += BtnOk_Click;
    }

    private void BtnOk_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUrl.Text))
        {
            MessageBox.Show("Please enter an image URL (e.g. https://.../picture.png).",
                "Image URL required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        ImageUrl = txtUrl.Text.Trim();
        ImgWidth = (int)numWidth.Value;
        ImgHeight = (int)numHeight.Value;
        DialogResult = DialogResult.OK;
        Close();
    }
}
