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
using System.Runtime.InteropServices;

public class EntryPoint
{
    ScriptPortal.Vegas.Vegas myVegas = null;
    String toalLength = null;
    String projectPath = null;
    String outputFilename = null;
    RenderTemplate renderTemplate = null;
    String dbgmsg = null;

    public void FromVegas(Vegas vegas)
    {
        myVegas = vegas;
        if (myVegas.Project.Length.ToMilliseconds() == 0)
        {
            MessageBox.Show("Project not loaded");
            throw new ApplicationException("Project not loaded");
        }

        GetRenderTemplate("MAGIX AVC/AAC MP4");
        CreateOutputFilename();
        RenderProject();
        myVegas.Exit();
    }

    /**
     * Generates output filename from project and output filetype
     * */
    public void CreateOutputFilename()
    {
        this.projectPath = this.myVegas.Project.FilePath;
        String dir = Path.GetDirectoryName(this.projectPath);
        String fileName = Path.GetFileNameWithoutExtension(this.projectPath);

        if (this.renderTemplate == null) throw new ApplicationException("Didnt have a rendertemplate to create filename from");
        String ext = Path.GetExtension(this.renderTemplate.FileExtensions[0]);
        fileName += ext;
        this.outputFilename = fileName;
    }

    /**
     * Get render template from project
     * */
    public bool GetRenderTemplate(String rendererName)
    {
        Renderer render = myVegas.Renderers.FindByName(rendererName);
        render.Templates.Refresh();
        RenderTemplate[] tmpRenderTemplate = render.Templates.FindProjectMatches(myVegas.Project);
        this.renderTemplate = tmpRenderTemplate[0];

        if (!renderTemplate.IsValid())
        {
            throw new ApplicationException("Project didnt have a valid render template");
        }
        return true;
    }
    /**
     * Now Render the project
     * */
    public void RenderProject()
    {

        String totalLength = myVegas.Project.Length.ToString();
        dbg("\n projectpath:");
        dbg(projectPath);
        dbg("\n dir:");
        dbg(projectPath);
        dbg("\n length:");
        dbg(totalLength);
        dbg("\n Template description");
        dbg(this.renderTemplate.Description);
        dbg("\n output filename:");
        dbg(this.outputFilename);

        RenderStatus status = myVegas.Project.Render(projectPath + outputFilename, renderTemplate);
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
                msg.Append(outputFilename);
                msg.Append("\n    Template: ");
                msg.Append(renderTemplate.Name);
                throw new ApplicationException(msg.ToString());
        }
    }

    public void dbg(String message)
    {
        this.dbgmsg = this.dbgmsg  + message + "\n";
    }

    public void showDebug()
    {
        MessageBox.Show(this.dbgmsg);
    }

    public String FixFileName(String name)
    {
        const Char replacementChar = '-';
        foreach (char badChar in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(badChar, replacementChar);
        }
        return name;
    }

    public void ValidateFilePath(String filePath)
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