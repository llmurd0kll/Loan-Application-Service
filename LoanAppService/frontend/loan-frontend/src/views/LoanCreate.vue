<template>
  <div>
    <h2>Создать заявку</h2>
    <el-form :model="form" label-width="150px">
      <el-form-item label="Номер заявки">
        <el-input v-model="form.number" />
      </el-form-item>
      <el-form-item label="Сумма">
        <el-input-number v-model="form.amount" :min="1" />
      </el-form-item>
      <el-form-item label="Срок">
        <el-input-number v-model="form.termValue" :min="1" />
      </el-form-item>
      <el-form-item label="Процентная ставка">
        <el-input-number v-model="form.interestValue" :min="1" :step="0.1" />
      </el-form-item>
      <el-button type="primary" @click="createLoan">Создать заявку</el-button>
    </el-form>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import api from '../api'

const form = reactive({
  number: '',
  amount: null,
  termValue: null,
  interestValue: null
})

async function createLoan() {
  if (!form.number || form.amount <= 0 || form.termValue <= 0 || form.interestValue <= 0) {
    alert('Проверьте правильность введённых данных')
    return
  }

  await api.post('/loans', form)
  alert('Заявка создана!')
}
</script>
