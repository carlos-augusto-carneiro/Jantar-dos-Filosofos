FROM mcr.microsoft.com/dotnet/framework/wcf:4.7.2-windowsservercore-ltsc2019

RUN powershell -Command \
    Invoke-WebRequest -Uri https://go.microsoft.com/fwlink/?LinkId=863262 -OutFile dotnet-framework-installer.exe; \
    .\dotnet-framework-installer.exe /quiet /install; \
    Remove-Item -Force dotnet-framework-installer.exe

WORKDIR /app

COPY . .

RUN msbuild "Jantar dos Filosofos.sln" /p:Configuration=Release

ENV DISPLAY=host.docker.internal:0.0

ENTRYPOINT ["Jantar dos Filosofos.exe"] 