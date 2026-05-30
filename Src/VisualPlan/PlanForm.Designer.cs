namespace VisualPlan
{
	partial class PlanForm
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
			this.dataGridViewPlan = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.buttonSave = new System.Windows.Forms.Button();
			this.buttonReload = new System.Windows.Forms.Button();
			this.buttonLoad = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.labelNotPlayedWith = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.labelNotPlayedWithSelected = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.labelNotUsedBoys = new System.Windows.Forms.Label();
			this.labelNotUsedGirls = new System.Windows.Forms.Label();
			this.buttonPrintStats = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridViewPlan
			// 
			this.dataGridViewPlan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewPlan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
			this.dataGridViewPlan.Location = new System.Drawing.Point(13, 13);
			this.dataGridViewPlan.Name = "dataGridViewPlan";
			this.dataGridViewPlan.Size = new System.Drawing.Size(668, 613);
			this.dataGridViewPlan.TabIndex = 0;
			this.dataGridViewPlan.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridViewPlan_CellMouseClick);
			this.dataGridViewPlan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPlan_KeyDown);
			this.dataGridViewPlan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGridViewPlan_KeyUp);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "1";
			this.Column1.Name = "Column1";
			this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column1.Width = 60;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "2";
			this.Column2.Name = "Column2";
			this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column2.Width = 60;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "3";
			this.Column3.Name = "Column3";
			this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column3.Width = 60;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "4";
			this.Column4.Name = "Column4";
			this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column4.Width = 60;
			// 
			// Column5
			// 
			this.Column5.HeaderText = "5";
			this.Column5.Name = "Column5";
			this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column5.Width = 60;
			// 
			// Column6
			// 
			this.Column6.HeaderText = "6";
			this.Column6.Name = "Column6";
			this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column6.Width = 60;
			// 
			// Column7
			// 
			this.Column7.HeaderText = "7";
			this.Column7.Name = "Column7";
			this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column7.Width = 60;
			// 
			// Column8
			// 
			this.Column8.HeaderText = "8";
			this.Column8.Name = "Column8";
			this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column8.Width = 60;
			// 
			// Column9
			// 
			this.Column9.HeaderText = "9";
			this.Column9.Name = "Column9";
			this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column9.Width = 60;
			// 
			// Column10
			// 
			this.Column10.HeaderText = "10";
			this.Column10.Name = "Column10";
			this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Column10.Width = 60;
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSave.Location = new System.Drawing.Point(606, 669);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Size = new System.Drawing.Size(75, 23);
			this.buttonSave.TabIndex = 1;
			this.buttonSave.Text = "Save";
			this.buttonSave.UseVisualStyleBackColor = true;
			// 
			// buttonReload
			// 
			this.buttonReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonReload.Location = new System.Drawing.Point(525, 669);
			this.buttonReload.Name = "buttonReload";
			this.buttonReload.Size = new System.Drawing.Size(75, 23);
			this.buttonReload.TabIndex = 2;
			this.buttonReload.Text = "Reload";
			this.buttonReload.UseVisualStyleBackColor = true;
			this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
			// 
			// buttonLoad
			// 
			this.buttonLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonLoad.Location = new System.Drawing.Point(444, 669);
			this.buttonLoad.Name = "buttonLoad";
			this.buttonLoad.Size = new System.Drawing.Size(75, 23);
			this.buttonLoad.TabIndex = 3;
			this.buttonLoad.Text = "Load...";
			this.buttonLoad.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 684);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(83, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Not played with:";
			// 
			// labelNotPlayedWith
			// 
			this.labelNotPlayedWith.AutoSize = true;
			this.labelNotPlayedWith.Location = new System.Drawing.Point(102, 684);
			this.labelNotPlayedWith.Name = "labelNotPlayedWith";
			this.labelNotPlayedWith.Size = new System.Drawing.Size(35, 13);
			this.labelNotPlayedWith.TabIndex = 5;
			this.labelNotPlayedWith.Text = "label2";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(10, 634);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(132, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Not played with (selected):";
			// 
			// labelNotPlayedWithSelected
			// 
			this.labelNotPlayedWithSelected.AutoSize = true;
			this.labelNotPlayedWithSelected.Location = new System.Drawing.Point(151, 634);
			this.labelNotPlayedWithSelected.Name = "labelNotPlayedWithSelected";
			this.labelNotPlayedWithSelected.Size = new System.Drawing.Size(35, 13);
			this.labelNotPlayedWithSelected.TabIndex = 7;
			this.labelNotPlayedWithSelected.Text = "label3";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(10, 650);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(78, 13);
			this.label3.TabIndex = 8;
			this.label3.Text = "Not used boys:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(10, 667);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(74, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Not used girls:";
			// 
			// labelNotUsedBoys
			// 
			this.labelNotUsedBoys.AutoSize = true;
			this.labelNotUsedBoys.Location = new System.Drawing.Point(94, 650);
			this.labelNotUsedBoys.Name = "labelNotUsedBoys";
			this.labelNotUsedBoys.Size = new System.Drawing.Size(75, 13);
			this.labelNotUsedBoys.TabIndex = 9;
			this.labelNotUsedBoys.Text = "Not used boys";
			// 
			// labelNotUsedGirls
			// 
			this.labelNotUsedGirls.AutoSize = true;
			this.labelNotUsedGirls.Location = new System.Drawing.Point(90, 667);
			this.labelNotUsedGirls.Name = "labelNotUsedGirls";
			this.labelNotUsedGirls.Size = new System.Drawing.Size(71, 13);
			this.labelNotUsedGirls.TabIndex = 10;
			this.labelNotUsedGirls.Text = "Not used girls";
			// 
			// buttonPrintStats
			// 
			this.buttonPrintStats.Location = new System.Drawing.Point(363, 669);
			this.buttonPrintStats.Name = "buttonPrintStats";
			this.buttonPrintStats.Size = new System.Drawing.Size(75, 23);
			this.buttonPrintStats.TabIndex = 11;
			this.buttonPrintStats.Text = "Stats";
			this.buttonPrintStats.UseVisualStyleBackColor = true;
			this.buttonPrintStats.Click += new System.EventHandler(this.buttonPrintStats_Click);
			// 
			// PlanForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 706);
			this.Controls.Add(this.buttonPrintStats);
			this.Controls.Add(this.labelNotUsedGirls);
			this.Controls.Add(this.labelNotUsedBoys);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.labelNotPlayedWithSelected);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.labelNotPlayedWith);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.buttonLoad);
			this.Controls.Add(this.buttonReload);
			this.Controls.Add(this.buttonSave);
			this.Controls.Add(this.dataGridViewPlan);
			this.Name = "PlanForm";
			this.Text = "Play Planner";
			this.Load += new System.EventHandler(this.PlanForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlan)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridViewPlan;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonReload;
		private System.Windows.Forms.Button buttonLoad;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelNotPlayedWith;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelNotPlayedWithSelected;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label labelNotUsedBoys;
		private System.Windows.Forms.Label labelNotUsedGirls;
		private System.Windows.Forms.Button buttonPrintStats;
	}
}

