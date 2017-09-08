FROM golang

ENV PORT 8080

RUN git clone https://github.com/prometheus/client_golang

WORKDIR client_golang/examples/random

RUN go get -d && go build

CMD echo "Listening on ${PORT}" && ./random -listen-address=:${PORT}
