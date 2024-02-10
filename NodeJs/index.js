const express = require("express");
const bodyParser = require("body-parser");

app = express();

app.use(bodyParser.json());

const data = [];
let id = 1;

app.get("/api/node", (req, res) => {
  res.send(data);
});

app.get("/api/node/get-from-dotnet", async (req, res) => {
  const results = [];
  const response = await fetch("http://localhost:5178/api/dotnet");
  const data = await response.json();
  console.log(response.status);
  console.log(data);
  results.push(data);
  res.send(results);
});

app.post("/api/node", (req, res) => {
  const person = req.body;
  data.push(person);
  res.sendStatus(200);
});

app.listen(3000, () => console.log("App running!"));
