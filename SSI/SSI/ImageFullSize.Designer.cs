namespace SSI
{
    partial class ImageFullSize
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closeUc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // closeUc
            // 
            this.closeUc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeUc.BackColor = System.Drawing.Color.Transparent;
            this.closeUc.BackgroundImage = global::SSI.Properties.Resources.Minimize_icon;
            this.closeUc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.closeUc.FlatAppearance.BorderSize = 0;
            this.closeUc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeUc.Location = new System.Drawing.Point(538, 3);
            this.closeUc.Name = "closeUc";
            this.closeUc.Size = new System.Drawing.Size(47, 48);
            this.closeUc.TabIndex = 0;
            this.closeUc.UseVisualStyleBackColor = false;
            this.closeUc.Click += new System.EventHandler(this.closeUc_Click);
            // 
            // ImageFullSize
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.closeUc);
            this.Name = "ImageFullSize";
            this.Size = new System.Drawing.Size(588, 312);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeUc;
    }
}
