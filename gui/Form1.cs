namespace gui;
using AdventureMode;

public partial class Form1 : Form
{
    public static int Count = 1;
    public Form1()
    {
        InitializeComponent();
        checkedListBox1.Items.Clear();
        Utility.Initialize();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (ConsoleApp.WRITER != null)
        {
            ConsoleApp.WRITER.Dispose();
        }
        ConsoleApp.WRITER = new StreamWriter("debug.csv");
        checkedListBox1.Items.Clear();
        IList<string> area = new List<string>();
        for (int n = 0; n < 5; n++)
        {
            if (n == 3)
            {
                if (Count == 2)
                {
                    for (int m = 0; m < 2; m++)
                    {
                        ConsoleApp.AddObjective(ref area, n);
                    }
                }
                else if (Count == 1)
                {
                    ConsoleApp.AddObjective(ref area, n);
                }
                else continue;
                continue;
            }
            for (int i = 0; i < Count; i++)
            {
                ConsoleApp.AddObjective(ref area, n);
            }
        }
        area = area.OrderBy(t => "Act").ToList();
        Console.WriteLine("Output of bounties:");
        foreach (string a in area)
        {
            checkedListBox1.Items.Add(a);
        }
        Console.WriteLine("\nDone.");
        ConsoleApp.WRITER.Flush();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Count = comboBox1.SelectedIndex;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        new Form2().ShowDialogAsync();
    }
}
