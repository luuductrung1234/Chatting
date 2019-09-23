const HUB_URL = 'https://localhost:44376/chattinghub';

let connection = null;

setupConnection = () => {
  connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Trace)
    .withUrl(HUB_URL, {
      skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
    })
    .build();

  connection.on('ReceiveSomethingsHappen', message => {
    console.log(message);
  });

  connection.on('finished', function() {
    connection.stop();
    console.log('>>>> Disconnected!');
  });

  connection
    .start()
    .then(() => console.log('>>>> Connected!'))
    .catch(err => console.error(err.toString()));

  connection.onclose(async () => {
    await restartConnection();
  });
};

setupConnection();

async function restartConnection() {
  try {
    await connection.start();
    console.log('>>>> Re-Connected!');
  } catch (err) {
    console.error(err);
    setTimeout(() => restartConnection(), 5000);
  }
}

document.getElementById('dosomethings').addEventListener('click', e => {
  e.preventDefault();

  connection.invoke('DoSomethings', 1);
});
