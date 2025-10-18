import { createRouter, createWebHistory } from 'vue-router'
import LoansList from '../views/LoansList.vue'
import LoanCreate from '../views/LoanCreate.vue'

const routes = [
  { path: '/', component: LoansList },
  { path: '/create', component: LoanCreate }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
