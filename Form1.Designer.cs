namespace CRC32_HashComparator
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
			this.button1_Folder1 = new System.Windows.Forms.Button();
			this.button2_Folder2 = new System.Windows.Forms.Button();
			this.textBox3_AnswerArea = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox2_Folder2 = new System.Windows.Forms.TextBox();
			this.textBox1_Folder1 = new System.Windows.Forms.TextBox();
			this.button3_GetAnswer = new System.Windows.Forms.Button();
			this.checkBox1_CompareAll = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// button1_Folder1
			// 
			this.button1_Folder1.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button1_Folder1.Location = new System.Drawing.Point(12, 12);
			this.button1_Folder1.Name = "button1_Folder1";
			this.button1_Folder1.Size = new System.Drawing.Size(270, 30);
			this.button1_Folder1.TabIndex = 0;
			this.button1_Folder1.Text = "Папка исходных файлов";
			this.button1_Folder1.UseVisualStyleBackColor = true;
			this.button1_Folder1.Click += new System.EventHandler(this.button1_Folder1_Click);
			// 
			// button2_Folder2
			// 
			this.button2_Folder2.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button2_Folder2.Location = new System.Drawing.Point(12, 48);
			this.button2_Folder2.Name = "button2_Folder2";
			this.button2_Folder2.Size = new System.Drawing.Size(270, 30);
			this.button2_Folder2.TabIndex = 2;
			this.button2_Folder2.Text = "Папка проверяемых файлов";
			this.button2_Folder2.UseVisualStyleBackColor = true;
			this.button2_Folder2.Click += new System.EventHandler(this.button2_Folder2_Click);
			// 
			// textBox3_AnswerArea
			// 
			this.textBox3_AnswerArea.BackColor = System.Drawing.SystemColors.Window;
			this.textBox3_AnswerArea.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox3_AnswerArea.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox3_AnswerArea.Location = new System.Drawing.Point(12, 172);
			this.textBox3_AnswerArea.Multiline = true;
			this.textBox3_AnswerArea.Name = "textBox3_AnswerArea";
			this.textBox3_AnswerArea.ReadOnly = true;
			this.textBox3_AnswerArea.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBox3_AnswerArea.Size = new System.Drawing.Size(909, 377);
			this.textBox3_AnswerArea.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(12, 138);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1077, 23);
			this.label1.TabIndex = 9;
			this.label1.Text = "Имя исходного файла                   | CRC32 исходного | CRC32 проверяемого | Ре" +
    "зультат проверки";
			// 
			// textBox2_Folder2
			// 
			this.textBox2_Folder2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox2_Folder2.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox2_Folder2.Location = new System.Drawing.Point(288, 54);
			this.textBox2_Folder2.Name = "textBox2_Folder2";
			this.textBox2_Folder2.ReadOnly = true;
			this.textBox2_Folder2.Size = new System.Drawing.Size(633, 20);
			this.textBox2_Folder2.TabIndex = 3;
			// 
			// textBox1_Folder1
			// 
			this.textBox1_Folder1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1_Folder1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textBox1_Folder1.Location = new System.Drawing.Point(288, 18);
			this.textBox1_Folder1.Name = "textBox1_Folder1";
			this.textBox1_Folder1.ReadOnly = true;
			this.textBox1_Folder1.Size = new System.Drawing.Size(633, 20);
			this.textBox1_Folder1.TabIndex = 1;
			// 
			// button3_GetAnswer
			// 
			this.button3_GetAnswer.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.button3_GetAnswer.Location = new System.Drawing.Point(12, 84);
			this.button3_GetAnswer.Name = "button3_GetAnswer";
			this.button3_GetAnswer.Size = new System.Drawing.Size(270, 30);
			this.button3_GetAnswer.TabIndex = 10;
			this.button3_GetAnswer.Text = "СРАВНИТЬ";
			this.button3_GetAnswer.UseVisualStyleBackColor = true;
			this.button3_GetAnswer.Click += new System.EventHandler(this.button3_GetAnswer_Click);
			// 
			// checkBox1_CompareAll
			// 
			this.checkBox1_CompareAll.AutoSize = true;
			this.checkBox1_CompareAll.Checked = true;
			this.checkBox1_CompareAll.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBox1_CompareAll.Location = new System.Drawing.Point(300, 90);
			this.checkBox1_CompareAll.Name = "checkBox1_CompareAll";
			this.checkBox1_CompareAll.Size = new System.Drawing.Size(463, 24);
			this.checkBox1_CompareAll.TabIndex = 11;
			this.checkBox1_CompareAll.Text = "Сравнить все файлы во всех вложенных директориях";
			this.checkBox1_CompareAll.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(932, 553);
			this.Controls.Add(this.checkBox1_CompareAll);
			this.Controls.Add(this.button3_GetAnswer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox3_AnswerArea);
			this.Controls.Add(this.textBox2_Folder2);
			this.Controls.Add(this.button2_Folder2);
			this.Controls.Add(this.textBox1_Folder1);
			this.Controls.Add(this.button1_Folder1);
			this.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(950, 600);
			this.MinimumSize = new System.Drawing.Size(950, 600);
			this.Name = "Form1";
			this.Text = "Хеш-компаратор CRC32";
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1_Folder1;
        private System.Windows.Forms.Button button2_Folder2;
        private System.Windows.Forms.TextBox textBox3_AnswerArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2_Folder2;
        private System.Windows.Forms.TextBox textBox1_Folder1;
        private System.Windows.Forms.Button button3_GetAnswer;
		private System.Windows.Forms.CheckBox checkBox1_CompareAll;
	}
}

