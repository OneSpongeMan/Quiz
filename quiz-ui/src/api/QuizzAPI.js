import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:54975/api/',
});

export const getQuizzes = () => api.get('quiz');
export const getQuizById = (id) => api.get(`quiz/${id}`);
export const createQuiz = (quizData) => api.post('quiz', quizData);
export const updateQuiz = (id, quizData) => api.put(`quiz/${id}`, quizData);
export const deleteQuiz = (id) => api.delete(`quiz/${id}`);