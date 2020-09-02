# Vegas-Script-Render

The goal of this project is to have scheduled renders that can run from a terminal. The script will then load the project and render it to the project folder. 
There will also be support for modifying the template, changing texts or clips or duration of scenes.

*  The first project template will generate music toplists.
*  Then the second project will generate commercials for clothes.
*  Third one we will se what it becomes.


## How to use

Change the path in the Render.bat to your vegas path. Then run the bat file with project file as the parameter.
  
  ```
  c:\Program Files\vegas\vegas170.exe -SCRIPT:"%~dp0\Render.cs" -SCRIPTARGS:"config.txt"
  ```
  
  
