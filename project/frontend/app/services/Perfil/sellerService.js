// app/services/Perfil/sellerService.js
import axios from 'axios';
import { tr } from 'vuetify/locale';

const API = 'http://localhost:5000/api/seller';

export const registrarVendedor = async (data) => {
  try {
    const answer = await axios.post(`${API}/register`, data);
    return answer.data;
  } catch(error) {
    console.error('Mensaje de error: ' + error.message);
    throw error;
  }
}

export const loginVendedor = (data) =>
  axios.post(`${API}/login`, data);

export const consultarVendedor = async (email) => {
  try {
    const response = await axios.get(`${API}/get`, {
      params: { email }
    });
    return response.data; // Retorna solo los datos del vendedor
  } catch (error) {
    console.error("Error en la API de Vendedor:", error.response?.status, error.message);
    return null; 
  }
};

export const updateVendedor = async (data) => {
  try {
    const response = await axios.post(`${API}/edit`, data);
  } catch (error) {
    console.error("Error en la API de Vendedor:", error.response?.status, error.message);
    return null; 
  }
}

