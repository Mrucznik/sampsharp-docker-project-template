
#
# Build gamemode stage
#
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-gamemode
RUN export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
WORKDIR /build

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet nuget add source http://nuget.timpotze.nl/api/v2/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out


#
# SAMP server stage
#
FROM ubuntu
# Install libraries
#RUN printf "deb http://archive.debian.org/debian/ jessie main\ndeb-src http://archive.debian.org/debian/ jessie main\ndeb http://security.debian.org jessie/updates main\ndeb-src http://security.debian.org jessie/updates main" > /etc/apt/sources.list
RUN apt-get install libc6 -y
RUN rm /var/cache/debconf/*
RUN apt-get update -y && apt-get upgrade -y
RUN dpkg --add-architecture i386
RUN apt-get install wget \
    unzip -y

# Get SAMP server
RUN mkdir /home/samp && \
    wget https://sa-mp.co.id/files/samp03DLsvr_R1.tar.gz && \
    tar -xzf samp03DLsvr_R1.tar.gz -C /home/samp --strip-components=1 && \ 
    rm samp03DLsvr_R1.tar.gz
    
# Get SAMP Sharp resources
RUN wget https://github.com/ikkentim/SampSharp/releases/download/0.9.3/SampSharp-0.9.3.zip && \
    unzip SampSharp-0.9.3.zip && \
    cp -r SampSharp-0.9.3/* /home/samp && \
    rm -r SampSharp-0.9.3 && rm SampSharp-0.9.3.zip

# Get .NET Core
RUN wget https://deploy.timpotze.nl/packages/dotnet20200127.zip && \
    unzip dotnet20200127.zip && \
    cp -r dotnet20200127/runtime /home/samp/dotnet && \
    rm -r dotnet20200127 && rm dotnet20200127.zip

# Change config
RUN sed -i 's/changeme/rconpassword/' /home/samp/server.cfg && \ 
    sed -i 's/SA-MP 0.3 Server/Samp Sharp docker server/' /home/samp/server.cfg && \ 
    sed -i 's/gamemode0 grandlarc 1/gamemode0 empty 1/' /home/samp/server.cfg && \
    sed -i 's/filterscripts base gl_actions gl_property gl_realtime/filterscripts/' /home/samp/server.cfg && \
    echo "plugins libSampSharp.so" >> /home/samp/server.cfg && \
    echo "coreclr dotnet" >> /home/samp/server.cfg && \
    echo "gamemode gamemode/Gamemode.dll" >> /home/samp/server.cfg

COPY --from=build-gamemode /build/out /home/samp/gamemode

VOLUME /home/samp/scriptfiles

EXPOSE 7777    

WORKDIR /home/samp
ENTRYPOINT ./samp03svr

RUN export DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1
