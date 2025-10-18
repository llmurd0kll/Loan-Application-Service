<template>
  <div>
    <h2>Список заявок</h2>

    <!-- Фильтры -->
    <el-form :inline="true" @submit.prevent>
      <el-form-item label="Статус">
        <el-select v-model="filters.status" placeholder="Все">
          <el-option label="Все" value=""></el-option>
          <el-option label="Опубликованные" value="Published"></el-option>
          <el-option label="Снятые" value="Unpublished"></el-option>
        </el-select>
      </el-form-item>

      <el-form-item label="Сумма от">
        <el-input-number v-model="filters.minAmount" :min="0" />
      </el-form-item>
      <el-form-item label="до">
        <el-input-number v-model="filters.maxAmount" :min="0" />
      </el-form-item>

      <el-form-item label="Срок от">
        <el-input-number v-model="filters.minTerm" :min="0" />
      </el-form-item>
      <el-form-item label="до">
        <el-input-number v-model="filters.maxTerm" :min="0" />
      </el-form-item>

      <el-button type="primary" @click="loadLoans">Применить</el-button>
    </el-form>

    <!-- Таблица -->
    <el-table :data="loans" style="width: 100%; margin-top: 20px;">
      <el-table-column prop="number" label="Номер" />
      <el-table-column prop="amount" label="Сумма" />
      <el-table-column prop="termValue" label="Срок" />
      <el-table-column prop="interestValue" label="Ставка (%)" />
      <el-table-column prop="status" label="Статус" />
      <el-table-column prop="createdAt" label="Создана" />
      <el-table-column label="Действие">
        <template #default="scope">
          <el-button size="small" type="warning" @click="toggleStatus(scope.row)">
            {{ scope.row.status === 'Published' ? 'Снять с публикации' : 'Опубликовать' }}
          </el-button>
        </template>
      </el-table-column>
    </el-table>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import api from '../api'

const loans = ref([])
const filters = ref({
  status: '',
  minAmount: null,
  maxAmount: null,
  minTerm: null,
  maxTerm: null
})

async function loadLoans() {
  const res = await api.get('/loans', { params: filters.value })
  loans.value = res.data
}

async function toggleStatus(loan) {
  await api.patch(`/loans/${loan.id}/toggle-status`)
  await loadLoans()
}

onMounted(loadLoans)
</script>

