namespace oci7
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.comph = new System.Windows.Forms.Button();
            this.pointcomp = new System.Windows.Forms.Button();
            this.homob = new System.Windows.Forms.Button();
            this.function1 = new System.Windows.Forms.Button();
            this.load2 = new System.Windows.Forms.Button();
            this.imagbox2 = new Emgu.CV.UI.ImageBox();
            this.rb3 = new System.Windows.Forms.RadioButton();
            this.rb2 = new System.Windows.Forms.RadioButton();
            this.rb1 = new System.Windows.Forms.RadioButton();
            this.dotsb = new System.Windows.Forms.Button();
            this.load = new System.Windows.Forms.Button();
            this.imagbox1 = new Emgu.CV.UI.ImageBox();
            this.webcambut = new System.Windows.Forms.Button();
            this.imagbox3 = new Emgu.CV.UI.ImageBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imagbox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagbox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagbox3)).BeginInit();
            this.SuspendLayout();
            // 
            // comph
            // 
            this.comph.Location = new System.Drawing.Point(297, 550);
            this.comph.Name = "comph";
            this.comph.Size = new System.Drawing.Size(84, 25);
            this.comph.TabIndex = 27;
            this.comph.Text = "rotate homo";
            this.comph.UseVisualStyleBackColor = true;
            this.comph.Click += new System.EventHandler(this.comph_Click);
            // 
            // pointcomp
            // 
            this.pointcomp.Location = new System.Drawing.Point(454, 498);
            this.pointcomp.Name = "pointcomp";
            this.pointcomp.Size = new System.Drawing.Size(65, 23);
            this.pointcomp.TabIndex = 26;
            this.pointcomp.Text = "draw line";
            this.pointcomp.UseVisualStyleBackColor = true;
            this.pointcomp.Click += new System.EventHandler(this.pointcomp_Click);
            // 
            // homob
            // 
            this.homob.Location = new System.Drawing.Point(297, 529);
            this.homob.Name = "homob";
            this.homob.Size = new System.Drawing.Size(78, 20);
            this.homob.TabIndex = 25;
            this.homob.Text = "make homo";
            this.homob.UseVisualStyleBackColor = true;
            this.homob.Click += new System.EventHandler(this.homob_Click);
            // 
            // function1
            // 
            this.function1.Location = new System.Drawing.Point(297, 498);
            this.function1.Name = "function1";
            this.function1.Size = new System.Drawing.Size(151, 23);
            this.function1.TabIndex = 24;
            this.function1.Text = "find points on second image";
            this.function1.UseVisualStyleBackColor = true;
            this.function1.Click += new System.EventHandler(this.function1_Click);
            // 
            // load2
            // 
            this.load2.Location = new System.Drawing.Point(12, 536);
            this.load2.Name = "load2";
            this.load2.Size = new System.Drawing.Size(106, 25);
            this.load2.TabIndex = 23;
            this.load2.Text = "open second img";
            this.load2.UseVisualStyleBackColor = true;
            this.load2.Click += new System.EventHandler(this.load2_Click);
            // 
            // imagbox2
            // 
            this.imagbox2.Location = new System.Drawing.Point(658, 12);
            this.imagbox2.Name = "imagbox2";
            this.imagbox2.Size = new System.Drawing.Size(640, 480);
            this.imagbox2.TabIndex = 22;
            this.imagbox2.TabStop = false;
            // 
            // rb3
            // 
            this.rb3.AutoSize = true;
            this.rb3.Location = new System.Drawing.Point(124, 544);
            this.rb3.Name = "rb3";
            this.rb3.Size = new System.Drawing.Size(80, 17);
            this.rb3.TabIndex = 21;
            this.rb3.Text = "fast method";
            this.rb3.UseVisualStyleBackColor = true;
            this.rb3.CheckedChanged += new System.EventHandler(this.rb3_CheckedChanged);
            // 
            // rb2
            // 
            this.rb2.AutoSize = true;
            this.rb2.Location = new System.Drawing.Point(124, 521);
            this.rb2.Name = "rb2";
            this.rb2.Size = new System.Drawing.Size(85, 17);
            this.rb2.TabIndex = 20;
            this.rb2.Text = "brisk method";
            this.rb2.UseVisualStyleBackColor = true;
            this.rb2.CheckedChanged += new System.EventHandler(this.rb2_CheckedChanged);
            // 
            // rb1
            // 
            this.rb1.AutoSize = true;
            this.rb1.Checked = true;
            this.rb1.Location = new System.Drawing.Point(124, 498);
            this.rb1.Name = "rb1";
            this.rb1.Size = new System.Drawing.Size(78, 17);
            this.rb1.TabIndex = 19;
            this.rb1.TabStop = true;
            this.rb1.Text = "gftt method";
            this.rb1.UseVisualStyleBackColor = true;
            this.rb1.CheckedChanged += new System.EventHandler(this.rb1_CheckedChanged);
            // 
            // dotsb
            // 
            this.dotsb.Location = new System.Drawing.Point(223, 498);
            this.dotsb.Name = "dotsb";
            this.dotsb.Size = new System.Drawing.Size(68, 23);
            this.dotsb.TabIndex = 18;
            this.dotsb.Text = "find points";
            this.dotsb.UseVisualStyleBackColor = true;
            this.dotsb.Click += new System.EventHandler(this.dotsb_Click);
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(12, 498);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(106, 23);
            this.load.TabIndex = 17;
            this.load.Text = "open img second";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.load_Click);
            // 
            // imagbox1
            // 
            this.imagbox1.Location = new System.Drawing.Point(12, 12);
            this.imagbox1.Name = "imagbox1";
            this.imagbox1.Size = new System.Drawing.Size(640, 480);
            this.imagbox1.TabIndex = 16;
            this.imagbox1.TabStop = false;
            // 
            // webcambut
            // 
            this.webcambut.Location = new System.Drawing.Point(525, 498);
            this.webcambut.Name = "webcambut";
            this.webcambut.Size = new System.Drawing.Size(75, 23);
            this.webcambut.TabIndex = 28;
            this.webcambut.Text = "webcam";
            this.webcambut.UseVisualStyleBackColor = true;
            this.webcambut.Click += new System.EventHandler(this.webcambut_Click);
            // 
            // imagbox3
            // 
            this.imagbox3.Location = new System.Drawing.Point(12, 12);
            this.imagbox3.Name = "imagbox3";
            this.imagbox3.Size = new System.Drawing.Size(1286, 480);
            this.imagbox3.TabIndex = 15;
            this.imagbox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(534, 528);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "catch surs";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(606, 498);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 30;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 587);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.webcambut);
            this.Controls.Add(this.comph);
            this.Controls.Add(this.imagbox3);
            this.Controls.Add(this.pointcomp);
            this.Controls.Add(this.homob);
            this.Controls.Add(this.function1);
            this.Controls.Add(this.load2);
            this.Controls.Add(this.imagbox2);
            this.Controls.Add(this.rb3);
            this.Controls.Add(this.rb2);
            this.Controls.Add(this.rb1);
            this.Controls.Add(this.dotsb);
            this.Controls.Add(this.load);
            this.Controls.Add(this.imagbox1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.imagbox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagbox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imagbox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button comph;
        private System.Windows.Forms.Button pointcomp;
        private System.Windows.Forms.Button homob;
        private System.Windows.Forms.Button function1;
        private System.Windows.Forms.Button load2;
        private Emgu.CV.UI.ImageBox imagbox2;
        private System.Windows.Forms.RadioButton rb3;
        private System.Windows.Forms.RadioButton rb2;
        private System.Windows.Forms.RadioButton rb1;
        private System.Windows.Forms.Button dotsb;
        private System.Windows.Forms.Button load;
        private Emgu.CV.UI.ImageBox imagbox1;
        private System.Windows.Forms.Button webcambut;
        private Emgu.CV.UI.ImageBox imagbox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

