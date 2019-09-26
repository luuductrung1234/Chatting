const HUB_URL =
  'http://localhost:19081/Chatting/Chatting.API/chattinghub';

let connection = null;

setupConnection = () => {
  connection = new signalR.HubConnectionBuilder()
    //.configureLogging(signalR.LogLevel.Trace)
    .withUrl(HUB_URL + `?userCode=${getSenderCode()}`, {
      //skipNegotiation: true,
      transport: signalR.HttpTransportType.WebSockets
      //transport: signalR.HttpTransportType.ServerSentEvents
      //transport: signalR.HttpTransportType.LongPolling
    })
    .build();

  connection.on('ReceiveSomethingsHappen', message => {
    console.log(message);
  });

  connection.on('ReceiveMessage', response => {
    console.log(`From ${response.senderCode} : ${response.message}`);
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

async function restartConnection() {
  try {
    await connection.start();
    console.log('>>>> Re-Connected!');
  } catch (err) {
    console.error(err);
    setTimeout(() => restartConnection(), 5000);
  }
}

document.getElementById('startConnect').addEventListener('click', e => {
  e.preventDefault();

  setupConnection();
});

document.getElementById('dosomethings').addEventListener('click', e => {
  e.preventDefault();

  connection.invoke('DoSomethings', 1);
});

document.getElementById('send').addEventListener('click', e => {
  e.preventDefault();

  var senderCode = getSenderCode();
  var receiverCode = getReceiverCode();
  var message = getMessage();

  connection.invoke('SendMessage', {
    senderCode: senderCode,
    receiverCode: receiverCode,
    message: message});
});

function getSenderCode() {
  // Selecting the input element and get its value
  return document.getElementById('senderCode').value;
}

function getReceiverCode() {
  // Selecting the input element and get its value
  return document.getElementById('receiverCode').value;
}

function getMessage() {
  // Selecting the input element and get its value
  return document.getElementById('message').value;
}