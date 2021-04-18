using System;
using Word = Microsoft.Office.Interop.Word;

namespace MakeReportWord
{
    internal class MakeReport
    {
        public void CreateReport(string faculty, string numberLab, string theme, string discipline, string professor, string year)
        {
            var end = Type.Missing;
            var app = new Word.Application();
            app.Visible = true;
            var doc = app.Documents.Add();
            var r = doc.Range();
            r.Font.Size = 14;
            r.Font.Name = "Times New Roman";
            r.Paragraphs.SpaceAfter = 0;
            r.Paragraphs.Space1();
            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            r.Text = "МИНИСТЕРСТВО НАУКИ И ВЫСШЕГО ОБРАЗОВАНИЯ";
            r.Text += "РОССИЙСКОЙ ФЕДЕРАЦИИ";
            r.Text += "ФЕДЕРАЛЬНОЕ ГОСУДАРСТВЕННОЕ БЮДЖЕТНОЕ";
            r.Text += "ОБРАЗОВАТЕЛЬНОЕ УЧРЕЖДЕНИЕ ВЫСШЕГО ОБРАЗОВАНИЯ";
            r.Text += "«ОРЛОВСКИЙ ГОСУДАРСТВЕННЫЙ УНИВЕРСИТЕТ";
            r.Text += "ИМЕНИ И.С.ТУРГЕНЕВА»" + SkipLine(1);
            r.Text += "Кафедра " + faculty + SkipLine(3);
            var LengthDoc = r.Text.Length;
            r.Text += "ОТЧЁТ";
            r = doc.Range(LengthDoc, end);
            r.Font.Size = 16;
            r.Font.Bold = 1;
            LengthDoc += r.Text.Length;
            r.Text += "По лабораторной работе №" + numberLab;
            r = doc.Range(LengthDoc, end);
            r.Paragraphs.SpaceAfter = 10;
            r.Font.Bold = 0;
            LengthDoc += r.Text.Length;
            r.Text += "на тему: «" + theme + "»";
            r = doc.Range(LengthDoc, end);
            r.Font.Size = 14;
            r.Paragraphs.SpaceAfter = 0;
            r.Text += "по дисциплине: «" + discipline + "»" + SkipLine(8);
            LengthDoc += r.Text.Length;
            r.Text += "Выполнили: Музалевский Н.С., Аллянов М.Д.";
            r.Text += "Институт приборостроения, автоматизации и информационных технологий";
            r.Text += "Направление: 09.03.04 «Программная инженерия»";
            r.Text += "Группа: 92ПГ";
            r = doc.Range(LengthDoc, end);
            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            LengthDoc += r.Text.Length;
            r.Text += "Проверил: " + professor;
            r = doc.Range(LengthDoc, end);
            r.Paragraphs.SpaceAfter = 10;
            LengthDoc += r.Text.Length;
            r.Text += SkipLine(1) + "Отметка о зачете: ";
            r = doc.Range(LengthDoc, end);
            r.Paragraphs.SpaceAfter = 0;
            LengthDoc += r.Text.Length;
            r.Text += "Дата: «____» __________ " + year + "г.";
            r = doc.Range(LengthDoc, end);
            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
            LengthDoc += r.Text.Length;
            r.Text += SkipLine(8) + "Орел, " + year;
            r = doc.Range(LengthDoc, end);
            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            LengthDoc += r.Text.Length - 1;
            r = doc.Range(LengthDoc, end);
            r.InsertBreak(0);
            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
        }
        string SkipLine(int quantity)
        {
            var str = string.Empty;
            for (var i = 0; i < quantity; i++)
            {
                str += Environment.NewLine;
            }
            return str;
        }
    }
}
