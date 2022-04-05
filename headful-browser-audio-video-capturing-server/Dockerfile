FROM node:17

RUN apt-get update && apt-get install -y \
    libnss3 \
    libxss1 \
    libasound2 \
    libatk-bridge2.0-0 \
    libgtk-3-0 \
    libgbm-dev \
    xvfb \
 && rm -rf /var/lib/apt/lists/*

# TODO: find a way to install Google Chrome without --fix-broken hack
RUN wget https://dl.google.com/linux/direct/google-chrome-unstable_current_amd64.deb \
 ; dpkg -i google-chrome-unstable_current_amd64.deb \
 ; apt-get -y update \
 ; apt-get -y upgrade \
 ; apt -y --fix-broken install

WORKDIR /usr/src/app

COPY package*.json ./

RUN npm ci --only=production

COPY . .

EXPOSE 8080

RUN chmod +x ./run.sh
CMD ["./run.sh"]
