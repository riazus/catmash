import React, { useState, useEffect } from 'react';
import { fetchCatPairApi, updateCatApi } from '../../api/Cat/Cat';

export default function Arena() {
  const [currentPair, setCurrentPair] = useState([]);

  useEffect(() => {
    fetchCatPair();
  }, []);

  const fetchCatPair = () => {
      fetchCatPairApi()
      .then((data) => {
        setCurrentPair(data);
      })
      .catch((error) => {
        console.log(error);
      })
    };

  const handleVote = (selectedCatIndex) => {
    const updatedCat = { ...currentPair[selectedCatIndex] };
    updatedCat.likeCount++;

    updateCatApi(updatedCat)
      .then(() => {
        fetchCatPair();
      })
      .catch((error) => {
        console.log(error);
      })
  };

  if (currentPair.length === 0) {
    return <div>Sorry, but cats count not enough.</div>;
  }

  const cat1 = currentPair[0];
  const cat2 = currentPair[1];

  return (
    <div>
      <h1>Arena</h1>
      <p>Choose the cutest cat:</p>
      <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
        <img
          src={cat1.imageUrl}
          alt="Cat 1"
          style={{ width: '300px', height: '300px', marginRight: '20px' }}
          onClick={() => handleVote(0)}
        />
        <p style={{ margin: '0 20px' }}>OR</p>
        <img
          src={cat2.imageUrl}
          alt="Cat 2"
          style={{ width: '300px', height: '300px', marginLeft: '20px' }}
          onClick={() => handleVote(1)}
        />
      </div>
    </div>
  );
}
