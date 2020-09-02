using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;

using ScriptPortal.Vegas;

public class EntryPoint
{
    ScriptPortal.Vegas.Vegas myVegas = null;

    public void FromVegas(Vegas vegas, String scriptFile, XmlDocument scriptSettings, ScriptArgs scriptArgs)
    {
        myVegas = vegas;

        StringBuilder dbgout = new StringBuilder("Info");
        //Timecode renderStart = Timecode;
        String totalLength = myVegas.Project.Length.ToString();

        String projectPath = myVegas.Project.FilePath;
        String dir = Path.GetDirectoryName(projectPath);
        String fileName = Path.GetFileNameWithoutExtension(projectPath);
        //String templateName = "Blu - ray 1920x1080 - 24p, 25Mbps video stream";
        Renderer render = myVegas.Renderers.FindByName("MAGIX AVC/AAC MP4");
        String dd = render.Name;
        render.Templates.Refresh();
        RenderTemplate[] tmpRenderTemplate = render.Templates.FindProjectMatches(myVegas.Project);
        String ext = Path.GetExtension(tmpRenderTemplate[0].FileExtensions[0]);
        fileName += ext;

        if (!tmpRenderTemplate[0].IsValid())
            throw new ApplicationException("Invalid template?");

        foreach (String s in scriptArgs)
        {
            dbgout.Append("arg: " + s + "\n");
        }
        dbgout.Append("\n projectpath:");
        dbgout.Append(projectPath);
        dbgout.Append("\n dir:");
        dbgout.Append(projectPath);
        dbgout.Append("\n length:");
        dbgout.Append(totalLength);
        dbgout.Append("\n Template description");
        dbgout.Append(tmpRenderTemplate[0].Description);
        dbgout.Append("\n output filename:");
        dbgout.Append(fileName);

        MessageBox.Show(dbgout.ToString());
        //fileName += fileextension;
        //dbgout.Append(fileName);
        //MessageBox.Show(dbgout.ToString());
        /*
        RenderStatus status = myVegas.Project.Render(projectPath+fileName, tmpRenderTemplate[0]);
        switch (status)
        {
            case RenderStatus.Complete:
                MessageBox.Show("Incredible, we did it...");
                break;
            case RenderStatus.Canceled:
                MessageBox.Show("cancel");
                break;
            case RenderStatus.Failed:
            default:
                StringBuilder msg = new StringBuilder("Render failed:\n");
                msg.Append("\n    file name: ");
                msg.Append(fileName);
                msg.Append("\n    Template: ");
                msg.Append(tmpRenderTemplate[0].Name);
                throw new ApplicationException(msg.ToString());
        }
        */
        //String filename = Path.Combine(dir,FixFileName(fileName),".", FixFileName(template.Extension));
        //args.OutputFile = filename;


    }


    String FixFileName(String name)
    {
        const Char replacementChar = '-';
        foreach (char badChar in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(badChar, replacementChar);
        }
        return name;
    }

    void ValidateFilePath(String filePath)
    {
        if (filePath.Length > 260)
            throw new ApplicationException("File name too long: " + filePath);
        foreach (char badChar in Path.GetInvalidPathChars())
        {
            if (0 <= filePath.IndexOf(badChar))
            {
                throw new ApplicationException("Invalid file name: " + filePath);
            }
        }
    }

}