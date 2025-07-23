## Running with Docker in Windows Container

Make sure Docker Desktop is running and set to use Windows containers.

Create a folder `C:/dev/windows-container-fonts-sandbox` on your host machine.

Run the following commands in PowerShell:

```powershell
docker build -f WindowsContainerFontsSandbox/Dockerfile -t yaskovdev/windows-container-fonts-sandbox .
docker run --name windows_container_fonts_sandbox -v C:/dev/windows-container-fonts-sandbox:C:/image -d yaskovdev/windows-container-fonts-sandbox
```

Check the `c:\dev\windows-container-fonts-sandbox\text_output.png` file. Note that the font is Segoe UI.

In the Dockerfile, comment out the lines marked with `# Comment out this line`, then build and run the container again.
Note that now the font is not Segoe UI, since it's not installed in the container.

The size of the image with the fonts is 6.35GB.
The size of the image without the fonts is 5.98GB.
