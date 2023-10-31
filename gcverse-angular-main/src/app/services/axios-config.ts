import { environment } from '../../environments/environment';
import axios from 'axios';

const axiosInstance = axios.create({
baseURL:  "https://localhost:7057",
  headers: {
    'Content-Type': 'application/json',
  },
});

export default axiosInstance;
