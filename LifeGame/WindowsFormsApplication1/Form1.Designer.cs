namespace LifeGame
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
            this.components = new System.ComponentModel.Container();
            this.lifeTimer = new System.Windows.Forms.Timer(this.components);
            this.startStopButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.randomizeButton = new System.Windows.Forms.Button();
            this.nameSpeedGeneration = new System.Windows.Forms.Label();
            this.fieldSizeText = new System.Windows.Forms.Label();
            this.stepButton = new System.Windows.Forms.Button();
            this.stepLabel = new System.Windows.Forms.Label();
            this.numericGenerationSpeed = new System.Windows.Forms.NumericUpDown();
            this.numericSizeOfField = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericGenerationSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeOfField)).BeginInit();
            this.SuspendLayout();
            // 
            // lifeTimer
            // 
            this.lifeTimer.Interval = 1000;
            this.lifeTimer.Tick += new System.EventHandler(this.lifeTimerTick);
            // 
            // startStopButton
            // 
            this.startStopButton.Location = new System.Drawing.Point(351, 711);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(75, 23);
            this.startStopButton.TabIndex = 0;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(445, 711);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // randomizeButton
            // 
            this.randomizeButton.Location = new System.Drawing.Point(538, 711);
            this.randomizeButton.Name = "randomizeButton";
            this.randomizeButton.Size = new System.Drawing.Size(75, 23);
            this.randomizeButton.TabIndex = 2;
            this.randomizeButton.Text = "Random";
            this.randomizeButton.UseVisualStyleBackColor = true;
            this.randomizeButton.Click += new System.EventHandler(this.randomizeButton_Click);
            // 
            // nameSpeedGeneration
            // 
            this.nameSpeedGeneration.AutoSize = true;
            this.nameSpeedGeneration.BackColor = System.Drawing.Color.White;
            this.nameSpeedGeneration.ForeColor = System.Drawing.Color.Black;
            this.nameSpeedGeneration.Location = new System.Drawing.Point(76, 716);
            this.nameSpeedGeneration.Name = "nameSpeedGeneration";
            this.nameSpeedGeneration.Size = new System.Drawing.Size(91, 13);
            this.nameSpeedGeneration.TabIndex = 3;
            this.nameSpeedGeneration.Text = "Generation speed";
            this.nameSpeedGeneration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fieldSizeText
            // 
            this.fieldSizeText.AutoSize = true;
            this.fieldSizeText.BackColor = System.Drawing.Color.White;
            this.fieldSizeText.ForeColor = System.Drawing.Color.Black;
            this.fieldSizeText.Location = new System.Drawing.Point(619, 716);
            this.fieldSizeText.Name = "fieldSizeText";
            this.fieldSizeText.Size = new System.Drawing.Size(50, 13);
            this.fieldSizeText.TabIndex = 5;
            this.fieldSizeText.Text = "Field size";
            // 
            // stepButton
            // 
            this.stepButton.Location = new System.Drawing.Point(254, 711);
            this.stepButton.Name = "stepButton";
            this.stepButton.Size = new System.Drawing.Size(75, 23);
            this.stepButton.TabIndex = 7;
            this.stepButton.Text = "Step";
            this.stepButton.UseVisualStyleBackColor = true;
            this.stepButton.Click += new System.EventHandler(this.stepButton_Click);
            // 
            // stepLabel
            // 
            this.stepLabel.AutoSize = true;
            this.stepLabel.BackColor = System.Drawing.Color.White;
            this.stepLabel.ForeColor = System.Drawing.Color.Black;
            this.stepLabel.Location = new System.Drawing.Point(12, 716);
            this.stepLabel.Name = "stepLabel";
            this.stepLabel.Size = new System.Drawing.Size(41, 13);
            this.stepLabel.TabIndex = 8;
            this.stepLabel.Text = "Step: 0";
            // 
            // numericGenerationSpeed
            // 
            this.numericGenerationSpeed.Location = new System.Drawing.Point(173, 714);
            this.numericGenerationSpeed.Name = "numericGenerationSpeed";
            this.numericGenerationSpeed.Size = new System.Drawing.Size(63, 20);
            this.numericGenerationSpeed.TabIndex = 9;
            this.numericGenerationSpeed.ValueChanged += new System.EventHandler(this.numericGenerationSpeed_ValueChanged);
            // 
            // numericSizeOfField
            // 
            this.numericSizeOfField.Location = new System.Drawing.Point(675, 714);
            this.numericSizeOfField.Name = "numericSizeOfField";
            this.numericSizeOfField.Size = new System.Drawing.Size(63, 20);
            this.numericSizeOfField.TabIndex = 10;
            this.numericSizeOfField.ValueChanged += new System.EventHandler(this.numericSizeOfField_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(817, 757);
            this.Controls.Add(this.numericSizeOfField);
            this.Controls.Add(this.numericGenerationSpeed);
            this.Controls.Add(this.stepLabel);
            this.Controls.Add(this.stepButton);
            this.Controls.Add(this.fieldSizeText);
            this.Controls.Add(this.nameSpeedGeneration);
            this.Controls.Add(this.randomizeButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.startStopButton);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericGenerationSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericSizeOfField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer lifeTimer;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button randomizeButton;
        private System.Windows.Forms.Label nameSpeedGeneration;
        private System.Windows.Forms.Label fieldSizeText;
        private System.Windows.Forms.Button stepButton;
        private System.Windows.Forms.Label stepLabel;
        private System.Windows.Forms.NumericUpDown numericGenerationSpeed;
        private System.Windows.Forms.NumericUpDown numericSizeOfField;
    }
}

