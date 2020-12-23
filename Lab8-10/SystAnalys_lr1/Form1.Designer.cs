namespace SystAnalys_lr1
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxMatrix = new System.Windows.Forms.ListBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.DrawFromMatrix = new System.Windows.Forms.Button();
            this.cycleButton = new System.Windows.Forms.Button();
            this.chainButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.buttonInc = new System.Windows.Forms.Button();
            this.buttonAdj = new System.Windows.Forms.Button();
            this.deleteALLButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.drawEdgeButton = new System.Windows.Forms.Button();
            this.drawVertexButton = new System.Windows.Forms.Button();
            this.sheet = new System.Windows.Forms.PictureBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.GetMatrix = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.getRandomMaitrix = new System.Windows.Forms.Button();
            this.depthFirstWalk = new System.Windows.Forms.Button();
            this.FindLoop = new System.Windows.Forms.Button();
            this.MaxFlow = new System.Windows.Forms.Button();
            this.SecondTask = new System.Windows.Forms.Button();
            this.ThirdTask = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxMatrix
            // 
            this.listBoxMatrix.FormattingEnabled = true;
            this.listBoxMatrix.Location = new System.Drawing.Point(614, 447);
            this.listBoxMatrix.Name = "listBoxMatrix";
            this.listBoxMatrix.Size = new System.Drawing.Size(312, 121);
            this.listBoxMatrix.TabIndex = 6;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(0, 0);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 23;
            // 
            // DrawFromMatrix
            // 
            this.DrawFromMatrix.Location = new System.Drawing.Point(850, 247);
            this.DrawFromMatrix.Name = "DrawFromMatrix";
            this.DrawFromMatrix.Size = new System.Drawing.Size(75, 51);
            this.DrawFromMatrix.TabIndex = 14;
            this.DrawFromMatrix.Text = "Draw";
            this.DrawFromMatrix.UseVisualStyleBackColor = true;
            this.DrawFromMatrix.Click += new System.EventHandler(this.DrawFromMatrix_Click);
            // 
            // cycleButton
            // 
            this.cycleButton.Image = global::SystAnalys_lr1.Properties.Resources.cycle;
            this.cycleButton.Location = new System.Drawing.Point(84, 64);
            this.cycleButton.Name = "cycleButton";
            this.cycleButton.Size = new System.Drawing.Size(70, 45);
            this.cycleButton.TabIndex = 11;
            this.cycleButton.UseVisualStyleBackColor = true;
            this.cycleButton.Click += new System.EventHandler(this.cycleButton_Click);
            // 
            // chainButton
            // 
            this.chainButton.Image = global::SystAnalys_lr1.Properties.Resources.chain;
            this.chainButton.Location = new System.Drawing.Point(84, 12);
            this.chainButton.Name = "chainButton";
            this.chainButton.Size = new System.Drawing.Size(70, 45);
            this.chainButton.TabIndex = 10;
            this.chainButton.UseVisualStyleBackColor = true;
            this.chainButton.Click += new System.EventHandler(this.chainButton_Click);
            // 
            // selectButton
            // 
            this.selectButton.Image = global::SystAnalys_lr1.Properties.Resources.cursor;
            this.selectButton.Location = new System.Drawing.Point(12, 12);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(45, 45);
            this.selectButton.TabIndex = 9;
            this.selectButton.UseVisualStyleBackColor = true;
            this.selectButton.Click += new System.EventHandler(this.selectButton_Click);
            // 
            // buttonInc
            // 
            this.buttonInc.Location = new System.Drawing.Point(833, 304);
            this.buttonInc.Name = "buttonInc";
            this.buttonInc.Size = new System.Drawing.Size(92, 34);
            this.buttonInc.TabIndex = 8;
            this.buttonInc.Text = "Intedential";
            this.buttonInc.UseVisualStyleBackColor = true;
            this.buttonInc.Click += new System.EventHandler(this.buttonInc_Click);
            // 
            // buttonAdj
            // 
            this.buttonAdj.Image = global::SystAnalys_lr1.Properties.Resources.smezh;
            this.buttonAdj.Location = new System.Drawing.Point(152, 252);
            this.buttonAdj.Name = "buttonAdj";
            this.buttonAdj.Size = new System.Drawing.Size(92, 52);
            this.buttonAdj.TabIndex = 7;
            this.buttonAdj.UseVisualStyleBackColor = true;
            this.buttonAdj.Click += new System.EventHandler(this.buttonAdj_Click);
            // 
            // deleteALLButton
            // 
            this.deleteALLButton.Image = global::SystAnalys_lr1.Properties.Resources.deleteAll;
            this.deleteALLButton.Location = new System.Drawing.Point(13, 217);
            this.deleteALLButton.Name = "deleteALLButton";
            this.deleteALLButton.Size = new System.Drawing.Size(45, 45);
            this.deleteALLButton.TabIndex = 5;
            this.deleteALLButton.UseVisualStyleBackColor = true;
            this.deleteALLButton.Click += new System.EventHandler(this.deleteALLButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Image = global::SystAnalys_lr1.Properties.Resources.delete;
            this.deleteButton.Location = new System.Drawing.Point(13, 166);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(45, 45);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // drawEdgeButton
            // 
            this.drawEdgeButton.Image = global::SystAnalys_lr1.Properties.Resources.edge;
            this.drawEdgeButton.Location = new System.Drawing.Point(12, 115);
            this.drawEdgeButton.Name = "drawEdgeButton";
            this.drawEdgeButton.Size = new System.Drawing.Size(45, 45);
            this.drawEdgeButton.TabIndex = 2;
            this.drawEdgeButton.UseVisualStyleBackColor = true;
            this.drawEdgeButton.Click += new System.EventHandler(this.drawEdgeButton_Click);
            // 
            // drawVertexButton
            // 
            this.drawVertexButton.Image = global::SystAnalys_lr1.Properties.Resources.vertex;
            this.drawVertexButton.Location = new System.Drawing.Point(13, 64);
            this.drawVertexButton.Name = "drawVertexButton";
            this.drawVertexButton.Size = new System.Drawing.Size(45, 45);
            this.drawVertexButton.TabIndex = 1;
            this.drawVertexButton.UseVisualStyleBackColor = true;
            this.drawVertexButton.Click += new System.EventHandler(this.drawVertexButton_Click);
            // 
            // sheet
            // 
            this.sheet.BackColor = System.Drawing.SystemColors.Control;
            this.sheet.Location = new System.Drawing.Point(0, 0);
            this.sheet.Name = "sheet";
            this.sheet.Size = new System.Drawing.Size(587, 577);
            this.sheet.TabIndex = 0;
            this.sheet.TabStop = false;
            this.sheet.MouseClick += new System.Windows.Forms.MouseEventHandler(this.sheet_MouseClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(613, 7);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(313, 229);
            this.dataGridView1.TabIndex = 15;
            // 
            // GetMatrix
            // 
            this.GetMatrix.Location = new System.Drawing.Point(613, 273);
            this.GetMatrix.Name = "GetMatrix";
            this.GetMatrix.Size = new System.Drawing.Size(75, 23);
            this.GetMatrix.TabIndex = 16;
            this.GetMatrix.Text = "GetMatrix";
            this.GetMatrix.UseVisualStyleBackColor = true;
            this.GetMatrix.Click += new System.EventHandler(this.GetMatrix_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(613, 247);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(75, 20);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "8";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(613, 302);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 34);
            this.button1.TabIndex = 19;
            this.button1.Text = "ListVertex";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(710, 302);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 34);
            this.button2.TabIndex = 20;
            this.button2.Text = "ListEdge";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // getRandomMaitrix
            // 
            this.getRandomMaitrix.Location = new System.Drawing.Point(822, 247);
            this.getRandomMaitrix.Name = "getRandomMaitrix";
            this.getRandomMaitrix.Size = new System.Drawing.Size(22, 23);
            this.getRandomMaitrix.TabIndex = 21;
            this.getRandomMaitrix.Text = "R";
            this.getRandomMaitrix.UseVisualStyleBackColor = true;
            this.getRandomMaitrix.Click += new System.EventHandler(this.getRandomMaitrix_Click);
            // 
            // depthFirstWalk
            // 
            this.depthFirstWalk.Location = new System.Drawing.Point(710, 354);
            this.depthFirstWalk.Name = "depthFirstWalk";
            this.depthFirstWalk.Size = new System.Drawing.Size(92, 34);
            this.depthFirstWalk.TabIndex = 22;
            this.depthFirstWalk.Text = "Depth Walk";
            this.depthFirstWalk.UseVisualStyleBackColor = true;
            this.depthFirstWalk.Click += new System.EventHandler(this.depthFirstWalk_Click);
            // 
            // FindLoop
            // 
            this.FindLoop.Location = new System.Drawing.Point(833, 349);
            this.FindLoop.Name = "FindLoop";
            this.FindLoop.Size = new System.Drawing.Size(92, 34);
            this.FindLoop.TabIndex = 25;
            this.FindLoop.Text = "FindLoop";
            this.FindLoop.UseVisualStyleBackColor = true;
            this.FindLoop.Click += new System.EventHandler(this.FindLoop_Click);
            // 
            // MaxFlow
            // 
            this.MaxFlow.Location = new System.Drawing.Point(339, 309);
            this.MaxFlow.Name = "MaxFlow";
            this.MaxFlow.Size = new System.Drawing.Size(92, 34);
            this.MaxFlow.TabIndex = 26;
            this.MaxFlow.Text = "First Task";
            this.MaxFlow.UseVisualStyleBackColor = true;
            // 
            // SecondTask
            // 
            this.SecondTask.Location = new System.Drawing.Point(339, 349);
            this.SecondTask.Name = "SecondTask";
            this.SecondTask.Size = new System.Drawing.Size(92, 34);
            this.SecondTask.TabIndex = 27;
            this.SecondTask.Text = "Second Task";
            this.SecondTask.UseVisualStyleBackColor = true;
            // 
            // ThirdTask
            // 
            this.ThirdTask.Location = new System.Drawing.Point(339, 389);
            this.ThirdTask.Name = "ThirdTask";
            this.ThirdTask.Size = new System.Drawing.Size(92, 34);
            this.ThirdTask.TabIndex = 28;
            this.ThirdTask.Text = "Third Task";
            this.ThirdTask.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(613, 408);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(312, 29);
            this.textBox2.TabIndex = 29;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(613, 342);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(75, 20);
            this.textBox3.TabIndex = 30;
            this.textBox3.Text = "1";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(613, 368);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(75, 20);
            this.textBox4.TabIndex = 31;
            this.textBox4.Text = "4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 580);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.sheet);
            this.Controls.Add(this.ThirdTask);
            this.Controls.Add(this.SecondTask);
            this.Controls.Add(this.MaxFlow);
            this.Controls.Add(this.FindLoop);
            this.Controls.Add(this.depthFirstWalk);
            this.Controls.Add(this.getRandomMaitrix);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.GetMatrix);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.DrawFromMatrix);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.cycleButton);
            this.Controls.Add(this.chainButton);
            this.Controls.Add(this.selectButton);
            this.Controls.Add(this.buttonInc);
            this.Controls.Add(this.buttonAdj);
            this.Controls.Add(this.listBoxMatrix);
            this.Controls.Add(this.deleteALLButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.drawEdgeButton);
            this.Controls.Add(this.drawVertexButton);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.sheet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox sheet;
        private System.Windows.Forms.Button drawVertexButton;
        private System.Windows.Forms.Button drawEdgeButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button deleteALLButton;
        private System.Windows.Forms.ListBox listBoxMatrix;
        private System.Windows.Forms.Button buttonAdj;
        private System.Windows.Forms.Button buttonInc;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.Button chainButton;
        private System.Windows.Forms.Button cycleButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button DrawFromMatrix;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button GetMatrix;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button getRandomMaitrix;
        private System.Windows.Forms.Button depthFirstWalk;
        private System.Windows.Forms.Button FindLoop;
        private System.Windows.Forms.Button MaxFlow;
        private System.Windows.Forms.Button SecondTask;
        private System.Windows.Forms.Button ThirdTask;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
    }
}

