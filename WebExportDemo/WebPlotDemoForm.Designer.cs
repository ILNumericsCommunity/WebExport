namespace WebExportDemo
{
    partial class WebPlotDemoForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebPlotDemoForm));
            splitContainer = new System.Windows.Forms.SplitContainer();
            comboBoxScene = new System.Windows.Forms.ComboBox();
            btnExportPlotly = new System.Windows.Forms.Button();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            ilPanel = new ILNumerics.Drawing.Panel();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // splitContainer
            // 
            splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer.IsSplitterFixed = true;
            splitContainer.Location = new System.Drawing.Point(0, 0);
            splitContainer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(comboBoxScene);
            splitContainer.Panel1.Controls.Add(btnExportPlotly);
            splitContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(splitContainer1);
            splitContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            splitContainer.Size = new System.Drawing.Size(1482, 753);
            splitContainer.SplitterDistance = 45;
            splitContainer.SplitterWidth = 2;
            splitContainer.TabIndex = 0;
            // 
            // comboBoxScene
            // 
            comboBoxScene.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBoxScene.FormattingEnabled = true;
            comboBoxScene.Location = new System.Drawing.Point(16, 9);
            comboBoxScene.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            comboBoxScene.Name = "comboBoxScene";
            comboBoxScene.Size = new System.Drawing.Size(215, 28);
            comboBoxScene.TabIndex = 2;
            comboBoxScene.SelectedIndexChanged += comboBoxScene_SelectedIndexChanged;
            // 
            // btnExportPlotly
            // 
            btnExportPlotly.AutoSize = true;
            btnExportPlotly.Location = new System.Drawing.Point(240, 8);
            btnExportPlotly.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btnExportPlotly.Name = "btnExportPlotly";
            btnExportPlotly.Size = new System.Drawing.Size(143, 30);
            btnExportPlotly.TabIndex = 1;
            btnExportPlotly.Text = "Export to Plotly";
            btnExportPlotly.UseVisualStyleBackColor = true;
            btnExportPlotly.Click += btnExportPlotly_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(ilPanel);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(webView);
            splitContainer1.Size = new System.Drawing.Size(1482, 706);
            splitContainer1.SplitterDistance = 753;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 1;
            // 
            // ilPanel
            // 
            ilPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            ilPanel.Editor = null;
            ilPanel.Location = new System.Drawing.Point(0, 0);
            ilPanel.Margin = new System.Windows.Forms.Padding(2);
            ilPanel.Name = "ilPanel";
            ilPanel.Rectangle = (System.Drawing.RectangleF)resources.GetObject("ilPanel.Rectangle");
            ilPanel.RendererType = ILNumerics.Drawing.RendererTypes.OpenGL;
            ilPanel.ShowUIControls = false;
            ilPanel.Size = new System.Drawing.Size(753, 706);
            ilPanel.TabIndex = 1;
            ilPanel.Timeout = 0U;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = System.Drawing.Color.White;
            webView.Dock = System.Windows.Forms.DockStyle.Fill;
            webView.Location = new System.Drawing.Point(0, 0);
            webView.Name = "webView";
            webView.Size = new System.Drawing.Size(724, 706);
            webView.TabIndex = 1;
            webView.ZoomFactor = 1D;
            // 
            // WebPlotDemoForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1482, 753);
            Controls.Add(splitContainer);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            MinimumSize = new System.Drawing.Size(848, 717);
            Name = "WebPlotDemoForm";
            Text = "Tikz Export Demo";
            Load += XPlotDemoForm_Load;
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel1.PerformLayout();
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button btnExportPlotly;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ILNumerics.Drawing.Panel ilPanel;
        private System.Windows.Forms.ComboBox comboBoxScene;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
    }
}

