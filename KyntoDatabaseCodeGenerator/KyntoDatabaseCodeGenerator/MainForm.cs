using System;
using System.Windows.Forms;

namespace KyntoDatabaseCodeGenerator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            // Define all of our strings.
            string Headers = "";
            string FieldsOutput = "";
            string FunctionsOutput = "";
            string Footers = "";

            string FinalOutput;

            #region Headers
            Headers += "using System;" + Environment.NewLine;
            Headers += "using System.Collections.Generic;" + Environment.NewLine;
            Headers += "using System.Linq;" + Environment.NewLine;
            Headers += "using System.Text;" + Environment.NewLine;
            Headers += "" + Environment.NewLine;
            Headers += "using KyntoLib.Interfaces.Database;" + Environment.NewLine;
            Headers += "using KyntoLib.Interfaces.Database.Query;" + Environment.NewLine;
            Headers += "using KyntoLib.Interfaces.Database.Tables;" + Environment.NewLine;
            Headers += "" + Environment.NewLine;
            Headers += "using KyntoDatabase.MySQL;" + Environment.NewLine;
            Headers += "using KyntoDatabase.MySQL.Query;" + Environment.NewLine;
            Headers += "" + Environment.NewLine;
            Headers += "using MySql.Data.MySqlClient;" + Environment.NewLine;
            Headers += "" + Environment.NewLine;
            Headers += "namespace KyntoDatabase.MySQL.Tables" + Environment.NewLine;
            Headers += "{" + Environment.NewLine;
            Headers += "" + Environment.NewLine;
            Headers += "    public class " + DatabaseNameInput.Text + "DatabaseTable : I" + DatabaseNameInput.Text + "DatabaseTable" + Environment.NewLine;
            Headers += "    {" + Environment.NewLine;
            Headers += "        /// <summary>" + Environment.NewLine;
            Headers += "        /// Stores the database handler." + Environment.NewLine;
            Headers += "        /// </summary>" + Environment.NewLine;
            Headers += "        private MySQLDatabase _DatabaseHandler;" + Environment.NewLine;
            Headers += Environment.NewLine;
            #endregion

            #region Fields
            // Define our code options.
            string[] DefinitionsBlocks = this.InputCode.Text.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.None);
            string[] NewDefinitionsBlocks = new string[DefinitionsBlocks.Length];
            string[] BlockNames = new string[DefinitionsBlocks.Length];
            string[] BlockTypes = new string[DefinitionsBlocks.Length];

            // Get the name & types of each block.
            for (int i = 0; i < DefinitionsBlocks.Length; i++)
            {
                // Split the block into lines & trim each line.
                string[] BlockLines = DefinitionsBlocks[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i2 = 0; i2 < BlockLines.Length; i2++)
                {
                    BlockLines[i2] = BlockLines[i2].Trim();
                }

                // The first part of the third line should contain the type.
                BlockTypes[i] = BlockLines[3].Split(' ')[0];
                // The second part will contain the name.
                BlockNames[i] = BlockLines[3].Split(' ')[1];
            }

            // Recompile each block.
            for (int i = 0; i < DefinitionsBlocks.Length; i++)
            {
                // Split the block into lines & trim each line.
                string[] BlockLines = DefinitionsBlocks[i].Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i2 = 0; i2 < BlockLines.Length; i2++)
                {
                    BlockLines[i2] = BlockLines[i2].Trim();
                }

                // Add the private bit.
                NewDefinitionsBlocks[i] += BlockLines[0] + Environment.NewLine + BlockLines[1] + Environment.NewLine + BlockLines[2] + Environment.NewLine;
                NewDefinitionsBlocks[i] += "private " + BlockTypes[i] + " _" + BlockNames[i] + ";";
                NewDefinitionsBlocks[i] += Environment.NewLine;
                NewDefinitionsBlocks[i] += Environment.NewLine;

                // Now we add the actual segment.
                NewDefinitionsBlocks[i] += BlockLines[0] + Environment.NewLine + BlockLines[1] + Environment.NewLine + BlockLines[2] + Environment.NewLine + "public " + BlockLines[3].Replace(" { get; set; }", "") + Environment.NewLine;
                NewDefinitionsBlocks[i] += "{" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "    get" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "    {" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "        return this._" + BlockNames[i] + ";" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "    }" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "    set" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "    {" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "        this._" + BlockNames[i] + " = value;" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "    }" + Environment.NewLine;
                NewDefinitionsBlocks[i] += "}" + Environment.NewLine;

                if (i != DefinitionsBlocks.Length - 1)
                {
                    NewDefinitionsBlocks[i] += Environment.NewLine;
                }
            }

            // Print output.
            for (int i = 0; i < NewDefinitionsBlocks.Length; i++)
            {
                FieldsOutput += NewDefinitionsBlocks[i];
            }

            // Add padding to each line.
            string[] FieldsOutputLines = FieldsOutput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            FieldsOutput = "";
            for (int i = 0; i < FieldsOutputLines.Length; i++)
            {
                FieldsOutput += "        " + FieldsOutputLines[i] + Environment.NewLine;
            }
            #endregion

            #region Functions
            // Generate constructor functions.
            FunctionsOutput += "/// <summary>" + Environment.NewLine + "/// Initialises this database table." + Environment.NewLine + "/// <param name=\"DatabaseHandler\">The database handler.</param>" + Environment.NewLine;
            FunctionsOutput += "public " + DatabaseNameInput.Text + "DatabaseTable(MySQLDatabase DatabaseHandler)" + Environment.NewLine;
            FunctionsOutput += "{" + Environment.NewLine + "    // Store." + Environment.NewLine + "    this._DatabaseHandler = DatabaseHandler;" + Environment.NewLine + "}" + Environment.NewLine;
            FunctionsOutput += Environment.NewLine;
            FunctionsOutput += "/// <summary>" + Environment.NewLine + "/// Initialises this database table." + Environment.NewLine + "/// </summary>" + Environment.NewLine;
            FunctionsOutput += "/// <param name=\"DatabaseHandler\">The database handler.</param>" + Environment.NewLine + "/// <param name=\"Data\">The data stores in this table.</param>" + Environment.NewLine;
            FunctionsOutput += "public " + DatabaseNameInput.Text + "DatabaseTable(MySQLDatabase DatabaseHandler, Dictionary<string, object> Data)" + Environment.NewLine;
            FunctionsOutput += "{" + Environment.NewLine + "    // Store." + Environment.NewLine + "    this._DatabaseHandler = DatabaseHandler;" + Environment.NewLine + "    FromDictionary(Data);" + Environment.NewLine;
            FunctionsOutput += "}" + Environment.NewLine;
            FunctionsOutput += Environment.NewLine;

            // Generate "FromDictionary" function, takes a dictionary and copies all its values into the class.
            FunctionsOutput += "/// <summary>" + Environment.NewLine;
            FunctionsOutput += "/// Fills this table with data from a dictionary." + Environment.NewLine;
            FunctionsOutput += "/// </summary>" + Environment.NewLine;
            FunctionsOutput += "/// <param name=\"Data\">The dictionary containing the data for this table.</param>" + Environment.NewLine;
            FunctionsOutput += "public void FromDictionary(Dictionary<string, object> Data)" + Environment.NewLine;
            FunctionsOutput += "{" + Environment.NewLine;
            FunctionsOutput += "    // Process the data." + Environment.NewLine;
            FunctionsOutput += "    foreach (KeyValuePair<string, object> DataObject in Data)" + Environment.NewLine;
            FunctionsOutput += "    {" + Environment.NewLine;
            FunctionsOutput += "        switch (DataObject.Key)" + Environment.NewLine;
            FunctionsOutput += "        {" + Environment.NewLine;

            // Loop through each possible field.
            for (int i = 0; i < BlockNames.Length; i++)
            {
                FunctionsOutput += "            case " + DatabaseNameInput.Text + "TableFields." + BlockNames[i] + ":" + Environment.NewLine;
                FunctionsOutput += "                this._" + BlockNames[i] + " = (" + BlockTypes[i] + ")DataObject.Value;" + Environment.NewLine;
                FunctionsOutput += "                break;" + Environment.NewLine;
                FunctionsOutput += Environment.NewLine;
            }

            FunctionsOutput += "            default:" + Environment.NewLine;
            FunctionsOutput += "                throw new Exception(\"Unknown database column \\\"\" + DataObject.Key + \"\\\"\");" + Environment.NewLine;
            FunctionsOutput += "        }" + Environment.NewLine;
            FunctionsOutput += "    }" + Environment.NewLine;
            FunctionsOutput += "}" + Environment.NewLine + Environment.NewLine;

            // Generate update function.
            FunctionsOutput += "/// <summary>" + Environment.NewLine;
            FunctionsOutput += "/// Creates a query that will update this table." + Environment.NewLine;
            FunctionsOutput += "/// </summary>" + Environment.NewLine;
            FunctionsOutput += "public IDatabaseQuery Update()" + Environment.NewLine;
            FunctionsOutput += "{" + Environment.NewLine;
            FunctionsOutput += "    if (this._Id == -1)" + Environment.NewLine;
            FunctionsOutput += "    {" + Environment.NewLine;
            FunctionsOutput += "        throw new Exception(\"This row has not been set an id, an update query cannot be generated.\");" + Environment.NewLine;
            FunctionsOutput += "    }" + Environment.NewLine;
            FunctionsOutput += "" + Environment.NewLine;
            FunctionsOutput += "    // Create the update query." + Environment.NewLine;
            FunctionsOutput += "    return this._DatabaseHandler.CreateQuery(DatabaseTables." + DatabaseNameInput.Text + ")" + Environment.NewLine;
            FunctionsOutput += "        .Update()" + Environment.NewLine;

            // Loop through each possible field.
            for (int i = 0; i < BlockNames.Length; i++)
            {
                FunctionsOutput += "        .Set(" + DatabaseNameInput.Text + "TableFields." + BlockNames[i] + ", this._" + BlockNames[i] + ")" + Environment.NewLine;
            }

            FunctionsOutput += "        .Where(" + DatabaseNameInput.Text + "TableFields.Id, DatabaseComparison.Equals, this._Id);" + Environment.NewLine;
            FunctionsOutput += "}" + Environment.NewLine + Environment.NewLine;

            // Generate save function.
            FunctionsOutput += "/// <summary>" + Environment.NewLine;
            FunctionsOutput += "/// Creates a query that will save this table" + Environment.NewLine;
            FunctionsOutput += "/// </summary>" + Environment.NewLine;
            FunctionsOutput += "public IDatabaseQuery Save()" + Environment.NewLine;
            FunctionsOutput += "{" + Environment.NewLine;
            FunctionsOutput += "    // Insert this into the database." + Environment.NewLine;
            FunctionsOutput += "    return this._DatabaseHandler.CreateQuery(DatabaseTables." + DatabaseNameInput.Text + ")" + Environment.NewLine;
            FunctionsOutput += "        .Insert()" + Environment.NewLine;

            // Loop through each possible field.
            for (int i = 0; i < BlockNames.Length; i++)
            {
                FunctionsOutput += "        .Values(" + DatabaseNameInput.Text + "TableFields." + BlockNames[i] + ", this._" + BlockNames[i] + ")" + Environment.NewLine;
            }

            FunctionsOutput += "}" + Environment.NewLine + Environment.NewLine;

            // Generate delete function.
            FunctionsOutput += "/// <summary>" + Environment.NewLine;
            FunctionsOutput += "/// Creates a query that will delete this table." + Environment.NewLine;
            FunctionsOutput += "/// </summary>" + Environment.NewLine;
            FunctionsOutput += "public IDatabaseQuery Delete()" + Environment.NewLine;
            FunctionsOutput += "{" + Environment.NewLine;
            FunctionsOutput += "    if (this._Id == -1)" + Environment.NewLine;
            FunctionsOutput += "    {" + Environment.NewLine;
            FunctionsOutput += "        throw new Exception(\"This row has not been set an id, a delete query cannot be generated.\");" + Environment.NewLine;
            FunctionsOutput += "    }" + Environment.NewLine;
            FunctionsOutput += "" + Environment.NewLine;
            FunctionsOutput += "    // Create the delete query." + Environment.NewLine;
            FunctionsOutput += "    return this._DatabaseHandler.CreateQuery(DatabaseTables." + DatabaseNameInput.Text + ")" + Environment.NewLine;
            FunctionsOutput += "        .Delete()" + Environment.NewLine;
            FunctionsOutput += "        .Where(" + DatabaseNameInput.Text + "TableFields.Id, DatabaseComparison.Equals, this._Id);" + Environment.NewLine;
            FunctionsOutput += "}";

            // Add padding to each line.
            string[] FunctionsOutputLines = FunctionsOutput.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            FunctionsOutput = "";
            for (int i = 0; i < FunctionsOutputLines.Length; i++)
            {
                FunctionsOutput += "        " + FunctionsOutputLines[i] + Environment.NewLine;
            }
            #endregion

            #region Footers
            Footers += "    }" + Environment.NewLine;
            Footers += "}" + Environment.NewLine;
            #endregion

            // Combine & output.
            FinalOutput = Headers + FieldsOutput + FunctionsOutput + Footers;
            Clipboard.SetText(FinalOutput, TextDataFormat.Text);
            this.OutputCode.Text = FinalOutput;
        }
    }
}
