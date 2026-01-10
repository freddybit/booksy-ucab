// app/services/Perfil/buyerService.js
import axios from 'axios';
import {Buyer} from '@assets/js/Buyer.js';

const API = 'http://localhost:5000/api/buyer';


export const registrarComprador = async (data) => {
  try {
    const answer = await axios.post(`${API}/register`, data);
    return answer.data;
  } catch(error) {
    console.error('Mensaje de error: ' + error.message);
    throw error;
  }
}

export const loginComprador = async (data) => {
  try {
    const answer = await axios.post(`${API}/login`, data);
    return answer;
  } catch (error) {
    console.error('Mensaje de error: ' + error.message);
    throw error;
  }
}

export const loadBuyer = async (email) => {
  try {
    const answer = await axios.get(API + '/getByEmail', { params: { email } });
    const b = answer.data;
    const buyer = new Buyer(b._email, b._firstName, b._lastName, b._age, b._password, b._purchaseHistory );
    return buyer;
  } catch(error) {
    console.error('Mensaje de error: ', error.message);
    throw error;
  }

}
