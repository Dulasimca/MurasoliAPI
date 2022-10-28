﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.IO;
using MurasoliAPI.Model;

namespace MurasoliAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        public Tuple<bool, string> AddFile()
        {
            try
            {
                ResizeImage resize = new ResizeImage();
                bool isCopied = false;
                string newFileName = string.Empty;
                string sFileName = string.Empty;
                var file = Request.Form.Files[0];
                //var sPath = Convert.ToString(Request.Form.Keys.Count[0]); //(new System.Collections.Generic.IDictionaryDebugView<string, Microsoft.Extensions.Primitives.StringValues>(((System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>.KeyCollection)((Microsoft.AspNetCore.Http.FormCollection)Request.Form).Keys)._dictionary).Items[0]).Value;
                if (file.Length > 0)
                {
                    var files = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var value = files.Split('^');
                    string fileName = string.Empty, folderName = string.Empty, newFilename = string.Empty;
                    if (value.Length > 2)
                    {
                        fileName = value[0];
                        folderName = value[1];
                        newFilename = value[2];
                    }
                    else
                    {
                        fileName = value[0];
                        folderName = value[1];
                        newFilename = value[0];
                    }

                    var folder = GlobalVariable.FolderPath + folderName; // Path.Combine("Resources", folderName);
                    var thump = GlobalVariable.FolderPath + "thump";
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    if (!Directory.Exists(thump))
                    {
                        Directory.CreateDirectory(thump);
                    }
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folder);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        isCopied = true;
                    }
                   // newFileName = fileName;
                    if (isCopied)
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(fullPath);
                        if (fi.Exists)
                        {
                            sFileName = DateTime.Now.ToString("ddMMyyyyhhmmss");
                            newFileName = fileName.Replace(fi.Extension, "_") + sFileName + fi.Extension;
                            var NewfullPath = Path.Combine(pathToSave, newFileName);
                            fi.MoveTo(NewfullPath);
                              resize.CompressImage(NewfullPath, thump, 25, newFileName);
                            //resize.ImgResize(NewfullPath, thump + "//" + newFileName);
                        }
                    }
                    return new Tuple<bool, string>(isCopied, newFileName);
                    // return Ok(new { dbPath });
                }
                else
                {
                    //return BadRequest();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //  return StatusCode(500, $"Internal server error: {ex}");
            }
            return new Tuple<bool, string>(false, "");

        }
    }
}
