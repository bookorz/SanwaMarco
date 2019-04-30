namespace MarcoTest
{
    partial class FormMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnTest = new System.Windows.Forms.Button();
            this.rtMsg = new System.Windows.Forms.RichTextBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.btnInit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTest.Location = new System.Drawing.Point(12, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(84, 39);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // rtMsg
            // 
            this.rtMsg.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.rtMsg.Location = new System.Drawing.Point(12, 57);
            this.rtMsg.Name = "rtMsg";
            this.rtMsg.RightMargin = 10000;
            this.rtMsg.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtMsg.Size = new System.Drawing.Size(1381, 381);
            this.rtMsg.TabIndex = 2;
            this.rtMsg.Text = "";
            this.rtMsg.WordWrap = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnOpen.Location = new System.Drawing.Point(102, 12);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(84, 39);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(218, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "泡沫排序";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConn
            // 
            this.btnConn.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnConn.Location = new System.Drawing.Point(313, 9);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(150, 39);
            this.btnConn.TabIndex = 0;
            this.btnConn.Text = "Connect";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(486, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(156, 38);
            this.button2.TabIndex = 4;
            this.button2.Text = "Send Message";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tbMsg
            // 
            this.tbMsg.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbMsg.Location = new System.Drawing.Point(648, 17);
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(602, 30);
            this.tbMsg.TabIndex = 5;
            this.tbMsg.Text = "$1SET:RESET";
            // 
            // btnInit
            // 
            this.btnInit.Font = new System.Drawing.Font("微軟正黑體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInit.Location = new System.Drawing.Point(1271, 8);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(90, 39);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1405, 450);
            this.Controls.Add(this.tbMsg);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rtMsg);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.btnConn);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnTest);
            this.Name = "FormMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.RichTextBox rtMsg;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button btnInit;
    }
}

