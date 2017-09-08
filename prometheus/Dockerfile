FROM alpine

COPY prometheus-1.7.1.linux-amd64.tar.gz .
RUN tar xvfz prometheus-*.tar.gz

RUN mkdir -p /prometheus \
    && cp -r prometheus-*/* prometheus

WORKDIR /prometheus

COPY prometheus.yml .
COPY prometheus.rules .

ENTRYPOINT ["./prometheus", "-config.file=prometheus.yml"]
