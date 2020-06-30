// Code Extracted & Simplified by misonothx
//
// Extracted from MKDS Course Modifier

using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

public class StaffGhostTime : Form
{
    // Form Design
    
	private void InitializeComponent()
	{
		comboBox1 = new ComboBox();
		label3 = new Label();
		label2 = new Label();
		label1 = new Label();
		numericUpDown3 = new NumericUpDown();
		numericUpDown2 = new NumericUpDown();
		numericUpDown1 = new NumericUpDown();
		base.SuspendLayout();
		comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
		comboBox1.FormattingEnabled = true;
		comboBox1.Items.AddRange(new object[]
		{
			"Figure-8 Circuit",
			"Yoshi Falls",
			"Cheep Cheep Beach",
			"Luigi’s Mansion",
			"Desert Hills",
			"Delfino Square",
			"Waluigi Pinball",
			"Shroom Ridge",
			"DK Pass",
			"Tick-Tock Clock",
			"Mario Circuit",
			"Airship Fortress",
			"Wario Stadium",
			"Peach Gardens",
			"Bowser Castle",
			"Rainbow Road",
			"SNES Mario Circuit 1",
			"N64 Moo Moo Farm",
			"GBA Peach Circuit",
			"GCN Luigi Circuit",
			"SNES Donut Plains 1",
			"N64 Frappe Snowland",
			"GBA Bowser Castle 2",
			"GCN Baby Park",
			"SNES Koopa Beach 2",
			"N64 Choco Mountain",
			"GBA Luigi Circuit",
			"GCN Mushroom Bridge",
			"SNES Choco Island 2",
			"N64 Banshee Boardwalk",
			"GBA Sky Garden",
			"GCN Yoshi Circuit"
		});
		comboBox1.Location = new Point(12, 12);
		comboBox1.Name = "comboBox1";
		comboBox1.Size = new Size(121, 21);
		comboBox1.TabIndex = 0;
		comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
		label3.AutoSize = true;
		label3.Location = new Point(168, 66);
		label3.Name = "label3";
		label3.Size = new Size(24, 13);
		label3.TabIndex = 15;
		label3.Text = "Ms:";
		label2.AutoSize = true;
		label2.Location = new Point(163, 40);
		label2.Name = "label2";
		label2.Size = new Size(29, 13);
		label2.TabIndex = 14;
		label2.Text = "Sec:";
		label1.AutoSize = true;
		label1.Location = new Point(165, 14);
		label1.Name = "label1";
		label1.Size = new Size(27, 13);
		label1.TabIndex = 13;
		label1.Text = "Min:";
		numericUpDown3.Location = new Point(198, 64);
		numericUpDown3.Maximum = 999
		numericUpDown3.Name = "numericUpDown3";
		numericUpDown3.Size = new Size(120, 20);
		numericUpDown3.TabIndex = 12;
		numericUpDown2.Location = new Point(198, 38);
		numericUpDown2.Maximum = 59;
		numericUpDown2.Name = "numericUpDown2";
		numericUpDown2.Size = new Size(120, 20);
		numericUpDown2.TabIndex = 11;
		numericUpDown1.Location = new Point(198, 12);
		numericUpDown1.Maximum = 15;
		numericUpDown1.Name = "numericUpDown1";
		numericUpDown1.Size = new Size(120, 20);
		numericUpDown1.TabIndex = 10;
		base.AutoScaleDimensions = new SizeF(6f, 13f);
		base.AutoScaleMode = AutoScaleMode.Font;
		base.ClientSize = new Size(330, 127);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.Controls.Add(numericUpDown3);
		base.Controls.Add(numericUpDown2);
		base.Controls.Add(numericUpDown1);
		base.Controls.Add(comboBox1);
		base.Name = "StaffGhostTime";
		Text = "StaffGhostTime";
		base.Load += StaffGhostTime_Load;
		base.ResumeLayout(false);
		base.PerformLayout();
	}
	
	// Actual Code
	
	public StaffGhostTime(string f)
	{
		InitializeComponent();
		file = f;
	}
	private void StaffGhostTime_Load(object sender, EventArgs e)
	{
		EndianBinaryReader endianBinaryReader = new EndianBinaryReader(File.OpenRead(file));
		endianBinaryReader.ReadBytes(32);
		for (int i = 0; i < 32; i++)
		{
			time[i] = new byte[4];
			time[i] = endianBinaryReader.ReadBytes(4);
		}
		comboBox1.SelectedIndex = 0;
		endianBinaryReader.Close();
	}
	private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
	{
		int num = parselittle(BitConverter.ToString(time[comboBox1.SelectedIndex]).Replace("-", ""));
		numericUpDown1.Value = TimeSpan.FromMilliseconds((double)Convert.ToDecimal(num * 1000)).Hours;
		numericUpDown2.Value = TimeSpan.FromMilliseconds((double)Convert.ToDecimal(num * 1000)).Minutes;
		numericUpDown3.Value = TimeSpan.FromMilliseconds((double)Convert.ToDecimal(num * 1000)).Seconds;
	}
	private int parselittle(string input)
	{
		return int.Parse(string.Concat(new string[]
		{
			input.ToCharArray()[6].ToString(),
			input.ToCharArray()[7].ToString(),
			input.ToCharArray()[4].ToString(),
			input.ToCharArray()[5].ToString(),
			input.ToCharArray()[2].ToString(),
			input.ToCharArray()[3].ToString(),
			input.ToCharArray()[0].ToString(),
			input.ToCharArray()[1].ToString()
		}), NumberStyles.HexNumber);
	}
	private byte[][] time = new byte[32][];
	private string file;
  }
}