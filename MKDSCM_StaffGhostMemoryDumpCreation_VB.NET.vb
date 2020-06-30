' Code Extracted & Simplified by misonothx
'
' Extracted from MKDS Course Modifier

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Globalization
Imports System.IO
Imports System.Windows.Forms

Public Class StaffGhostTime
    ' Form Design
    Inherits Form
   
	Private Sub InitializeComponent()
		comboBox1 = New ComboBox()
		label3 = New Label()
		label2 = New Label()
		label1 = New Label()
		numericUpDown3 = New NumericUpDown()
		numericUpDown2 = New NumericUpDown()
		numericUpDown1 = New NumericUpDown()
		MyBase.SuspendLayout()
		comboBox1.DropDownStyle = ComboBoxStyle.DropDownList
		comboBox1.FormattingEnabled = True
		      comboBox1.Items.AddRange(New Object() { "Figure-8 Circuit", "Yoshi Falls", "Cheep Cheep Beach", "Luigi’s Mansion", "Desert Hills", "Delfino Square", "Waluigi Pinball", "Shroom Ridge", "DK Pass", "Tick-Tock Clock", "Mario Circuit", "Airship Fortress", "Wario Stadium", "Peach Gardens", "Bowser Castle", "Rainbow Road", "SNES Mario Circuit 1", "N64 Moo Moo Farm", "GBA Peach Circuit", "GCN Luigi Circuit", "SNES Donut Plains 1", "N64 Frappe Snowland", "GBA Bowser Castle 2", "GCN Baby Park", "SNES Koopa Beach 2", "N64 Choco Mountain", "GBA Luigi Circuit", "GCN Mushroom Bridge", "SNES Choco Island 2", "N64 Banshee Boardwalk", "GBA Sky Garden", "GCN Yoshi Circuit" })
		comboBox1.Location = New Point(12, 12)
		comboBox1.Name = "comboBox1"
		comboBox1.Size = New Size(121, 21)
		comboBox1.TabIndex = 0
		AddHandler comboBox1.SelectedIndexChanged, AddressOf comboBox1_SelectedIndexChanged
		label3.AutoSize = True
		label3.Location = New Point(168, 66)
		label3.Name = "label3"
		label3.Size = New Size(24, 13)
		label3.TabIndex = 15
		label3.Text = "Ms:"
		label2.AutoSize = True
		label2.Location = New Point(163, 40)
		label2.Name = "label2"
		label2.Size = New Size(29, 13)
		label2.TabIndex = 14
		label2.Text = "Sec:"
		label1.AutoSize = True
		label1.Location = New Point(165, 14)
		label1.Name = "label1"
		label1.Size = New Size(27, 13)
		label1.TabIndex = 13
		label1.Text = "Min:"
		numericUpDown3.Location = New Point(198, 64)
		numericUpDown3.Maximum = 999
		numericUpDown3.Name = "numericUpDown3"
		numericUpDown3.Size = New Size(120, 20)
		numericUpDown3.TabIndex = 12
		numericUpDown2.Location = New Point(198, 38)
		numericUpDown2.Maximum = 59
		numericUpDown2.Name = "numericUpDown2"
		numericUpDown2.Size = New Size(120, 20)
		numericUpDown2.TabIndex = 11
		numericUpDown1.Location = New Point(198, 12)
		numericUpDown1.Maximum = 15
		numericUpDown1.Name = "numericUpDown1"
		numericUpDown1.Size = New Size(120, 20)
		numericUpDown1.TabIndex = 10
		MyBase.AutoScaleDimensions = New SizeF(6F, 13F)
		MyBase.AutoScaleMode = AutoScaleMode.Font
		MyBase.ClientSize = New Size(330, 127)
		MyBase.Controls.Add(label3)
		MyBase.Controls.Add(label2)
		MyBase.Controls.Add(label1)
		MyBase.Controls.Add(numericUpDown3)
		MyBase.Controls.Add(numericUpDown2)
		MyBase.Controls.Add(numericUpDown1)
		MyBase.Controls.Add(comboBox1)
		MyBase.Name = "StaffGhostTime"
		Text = "StaffGhostTime"
		AddHandler MyBase.Load, AddressOf StaffGhostTime_Load
		MyBase.ResumeLayout(False)
		MyBase.PerformLayout()
	End Sub

    ' Actual Code
    
	Public Sub New(f As String)
		InitializeComponent()
		file = f
	End Sub

	Private Sub StaffGhostTime_Load(sender As Object, e As EventArgs)
		Dim endianBinaryReader As EndianBinaryReader = New EndianBinaryReader(File.OpenRead(file))
		endianBinaryReader.ReadBytes(32)
		For i As Integer = 0 To 32 - 1
			time(i) = New Byte(3) {}
			time(i) = endianBinaryReader.ReadBytes(4)
		Next
		comboBox1.SelectedIndex = 0
		endianBinaryReader.Close()
	End Sub

	Private Sub comboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
		Dim num As Integer = parselittle(BitConverter.ToString(time(comboBox1.SelectedIndex)).Replace("-", ""))
		numericUpDown1.Value = TimeSpan.FromMilliseconds(CDbl(Convert.ToDecimal(num * 1000))).Hours
		numericUpDown2.Value = TimeSpan.FromMilliseconds(CDbl(Convert.ToDecimal(num * 1000))).Minutes
		numericUpDown3.Value = TimeSpan.FromMilliseconds(CDbl(Convert.ToDecimal(num * 1000))).Seconds
	End Sub

	Private Function parselittle(input As String) As Integer
		Return Integer.Parse(String.Concat(New String() { input.ToCharArray()(6).ToString(), input.ToCharArray()(7).ToString(), input.ToCharArray()(4).ToString(), input.ToCharArray()(5).ToString(), input.ToCharArray()(2).ToString(), input.ToCharArray()(3).ToString(), input.ToCharArray()(0).ToString(), input.ToCharArray()(1).ToString() }), NumberStyles.HexNumber)
	End Function
	Private time As Byte()() = New Byte(31)() {}
	Private file As String
End Class
