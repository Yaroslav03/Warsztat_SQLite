using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.Office.Interop;
using System.IO;


namespace Warsztat
{
    public partial class Form1 : System.Windows.Forms.Form
    {
      internal class Word
        {
            private FileInfo _fileInfo;

            public Word(string fileName)
            {
                if (File.Exists(fileName))
                {
                    _fileInfo = new FileInfo(fileName);

                }
                else
                {
                    throw new ArgumentException("Plik nie znałieziono");
                }
            }
            internal bool Process(Dictionary<string, string> items)
            {
                Microsoft.Office.Interop.Word.Application app = null;

                try
                {
                    app = new Microsoft.Office.Interop.Word.Application();
                    Object file = _fileInfo.FullName; //Шлях до файлу

                    Object missing = Type.Missing;

                    app.Documents.Open(file);   //Відкриваємо документ

                    foreach (var item in items)
                    {
                        Microsoft.Office.Interop.Word.Find find = app.Selection.Find;
                        find.Text = item.Key;
                        find.Replacement.Text = item.Value;

                        Object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                        Object replace = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;

                        find.Execute(FindText: Type.Missing,
                            MatchCase: false,
                            MatchWholeWord: false,
                            MatchWildcards: false,
                            MatchSoundsLike: missing,
                            MatchAllWordForms: false,
                            Forward: true,
                            Wrap: wrap,
                            Format: false,
                            ReplaceWith: missing, Replace: replace);
                    }
                    //Зберігаємо наш документ
                    Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyy") + _fileInfo.Name);
                    app.ActiveDocument.SaveAs2(newFileName);
                    //Друк
                    app.ActiveDocument.PrintPreview();
                    app.ActiveDocument.PrintOut();
                    //закриваємо
                    app.ActiveDocument.Close();

                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                finally
                {
                    if (app != null)
                    {
                        app.Quit();
                    }
                }
                return false;
            }
        }
    }
}