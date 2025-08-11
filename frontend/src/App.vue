<template>
  <div style="padding:20px; font-family:Arial">
    <h1>Sayaç Yönetim DEMO</h1>

    <!--Sayaç Listeleme kısmı-->
    <h2>Sayaç Listesi</h2>
    <table border="1" cellpadding="5" cellspacing="0">
      <thead> 
        <tr>
          <th>ID</th>
          <th>Numara</th>
          <th>Okuma</th>
          <th>Tarih</th>
          <th>İşlemler</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="sayac in sayaclar" :key="sayac.id">
          <td>{{ sayac.id }}</td>
          <td>{{ sayac.meterNumber }}</td>
          <td>{{ sayac.readingValue }}</td>
          <td>{{ formatDate(sayac.readingDate) }}</td>
          <td>
            <button @click="duzenle(sayac)">Düzenle</button>
            <button @click="sil(sayac.id)">Sil</button>
          </td>
        </tr>
      </tbody>
    </table>

    <!--Sayaç Güncelleme kısmı-->
    <h2>{{ form.id ? "Sayaç Güncelle" : "Yeni Sayaç Ekle" }}</h2>
    <form @submit.prevent="kaydet">
      <input v-model="form.meterNumber" placeholder="Sayaç numarası" required />
      <input v-model.number="form.readingValue" type="number" placeholder="Okuma değeri" required />
      <input v-model="form.readingDate" type="datetime-local" required />
      <button type="submit">Kaydet</button>
      <button type="button" @click="form = {}">Temizle</button>
    </form>
  </div>
</template>

<script>
import axios from "axios";
// API veri çekme kısmı
export default {
  name: "App",
  data() {
    return {
      sayaclar: [],
      form: {}
    };
  },
  methods: {
    // Listeleme
    async get() {
      const res = await axios.get("http://localhost:5050/api/sayac");
      this.sayaclar = res.data;
    },
    // Tarih formatı
    formatDate(dateStr) {
      const date = new Date(dateStr);
      return date.toLocaleString("tr-TR");
    },
    // Ekle/Güncelle
    async kaydet() {
      if (this.form.id) {
        await axios.put(`https://localhost:7129/api/sayac/${this.form.id}`, this.form);
      } else {
        await axios.post("https://localhost:7129/api/sayac", this.form);
      }
      this.form = {};
      this.get();
    },
    // Düzenleme
    duzenle(sayac) {
      this.form = { ...sayac };
      this.form.readingDate = this.form.readingDate.slice(0, 16); // input için format
    },
    // Silme
    async sil(id) {
      await axios.delete(`https://localhost:7129/api/sayac/${id}`);
      this.get();
    }
  },
  mounted() {
    this.get();
  }
};
</script>

<style>
table {
  margin-bottom: 20px;
}
button {
  margin-left: 5px;
}
</style>
