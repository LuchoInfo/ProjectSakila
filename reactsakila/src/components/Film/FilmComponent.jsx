// src/components/FilmComponent.jsx
import React, { useEffect } from 'react';
import axios from 'axios';

const FilmComponent = () => {
  useEffect(() => {
    // Realiza la solicitud HTTP
    axios.get('https://localhost:7002/api/Film/2', { withCredentials: true }) 
      .then(response => {
        // Imprime los datos en la consola
        console.log('Data from API:', response.data);
      })
      .catch(error => {
        // Imprime el error en la consola si ocurre uno
        console.error('Error fetching data:', error);
      });
  }, []);

  return (
    <div>
      <h1>Film Data</h1>
      <p>Check the console to see the API response.</p>
    </div>
  );
};

export default FilmComponent;
