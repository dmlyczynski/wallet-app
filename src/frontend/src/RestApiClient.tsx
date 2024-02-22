import axios from 'axios';

const headers = {
  'Content-Type': 'text/plain',
  'Accept' : '*/*'
};

const restApiClient = axios.create({
  baseURL: 'http://localhost:5000/api',
  headers: headers
});

export default restApiClient;