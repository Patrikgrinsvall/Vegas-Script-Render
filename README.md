# Vegas-Script-Render

The goal of this project is to have scheduled renders that can run from a terminal. The batfile will start vegas with the project and then run the script provided and the script will render the project with project default render settings to the project folder with the correct extension.

## How to use

Change the path in the Render.bat to your vegas path and change the path to the project as the second argument. Then run the bat file with project file as the parameter.
  
  ```
  c:\Program Files\vegas\vegas170.exe "c:\user\documents\myproject.veg" -SCRIPT:"%~dp0\ScriptRender\EntryPoint.cs"
  ```
  
  ## How to extend
  If you want to use this as a boiler plate for your own scripts, place it inside vegas install directory/Script Menu/EntryPoint.cs and then tools->scripts->EntryPoint.cs from inside Vegas.
  
