import { apiClient } from "../api";

export const fetchCatPairApi = async () => {
    const response = await apiClient.get(`cats/pair`);
    return response.data;
}

export const fetchCatApi = async () => {
    const response = await apiClient.get(`cats`);
    return response.data;
}

export const updateCatApi = async (cat) => {
    const response = await apiClient.put(`cats`, cat);
    return response.data;
}