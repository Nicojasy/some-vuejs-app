<template>
  <div>
    <div class="container">
      <div class="box">
        <div>
          <h4 class="mt-2 text-left box-text">Редактирование таблицы</h4>
          <button type="button" @click="showAddRecordModal" class="btn btn-success btn-sm box-button">
            Add new record
          </button>
        </div>
      </div>
      <div v-if="loading" class="mt-2 text-center">
        Загрузка...
      </div>
      <div v-else-if="error" class="mt-2 text-center">
        {{ error }}
      </div>
      <table v-else class="table table-bordered mt-6 cw-table-list">
        <thead>
          <tr>
            <th scope="col" class="table-first-el">#</th>
            <th scope="col">Фамилия</th>
            <th scope="col">Имя</th>
            <th scope="col">Отчество</th>
            <th scope="col">Возраст</th>
            <th scope="col" class="col-edit"></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(entry, i) in tableData" :key="i">
            <th scope="row" class="table-first-el">{{ ++i }}</th>
            <td>{{ entry.surname }}</td>
            <td>{{ entry.firstname }}</td>
            <td>{{ entry.patronymic }}</td>
            <td>{{ entry.age }}</td>
            <td align="center">
              <button type="button" @click="onEdit(i-1)" class="btn btn-warning btn-sm">
                edit
              </button>
              <button type="button" @click="onDelete(entry.id)" class="btn btn-danger btn-sm">
                delete
              </button>
            </td>
          </tr>
        </tbody>
      </table>
      <modal-window ref="addRecordModal" @positiveAction="Save">
        <template v-slot:title>
          Добавление записи
        </template>
        <template v-slot:positive-button>
          Добавить
        </template>
      </modal-window>
      <modal-window ref="editRecordModal" @positiveAction="Edit">
        <template v-slot:title>
          Изменение записи
        </template>
        <template v-slot:positive-button>
          Изменить
        </template>
      </modal-window>
    </div>
  </div>
</template>

<script>
  import ModalWindow from './ModalWindow.vue'
  import axios from 'axios'

  export default {
    name: 'EditTable',
    components: {
      ModalWindow
    },
    data: () => ({
      loading: false,
      error: null,
      tableData: []
    }),
    created() {
      this.fetchData();
    },
    methods: {
      fetchData() {
        this.error = null;
        this.loading = true;
        axios.get("https://localhost:44311/api/datatable")
          .then(response => {
            this.loading = false;
            this.tableData = response.data;
          })
          .catch(error => {
            this.loading = false;
            this.error = "Sorry! " + error.message;
            console.error("Get request failed!", error);
          });
      },
      onEdit(key) {
        console.log(key, this.tableData);
        let row = this.tableData[key];
        this.showEditRecordModal();
        this.$refs.editRecordModal.surname = row.surname;
        this.$refs.editRecordModal.firstname = row.firstname;
        this.$refs.editRecordModal.patronymic = row.patronymic;
        this.$refs.editRecordModal.age = row.age;
        this.$refs.editRecordModal.key = key;
      },
      onDelete(key) {
        axios.delete("https://localhost:44311/api/datatable", key)
          .then(() => {
            this.tableData.splice(key, 1);
          })
          .catch(error => {
            alert("Sorry! " + error.message);
            console.error("Delete request failed!", error);
          });
      },
      Save(newRecord) {
        axios.post("https://localhost:44311/api/datatable", newRecord)
          .then((response) => {
            this.tableData.push({
              id: response.id,
              surname: newRecord.surname,
              firstname: newRecord.firstname,
              patronymic: newRecord.patronymic,
              age: newRecord.age,
            });
          })
          .catch(error => {
            //this.$refs.addRecordModal.errorMessage = error.message;
            alert("Sorry! " + error.message);
            console.error("Post request failed!", error);
          });
      },
      Edit(editRecord, key) {
        axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';
        let id = this.tableData[key].id;
        console.log({ ...editRecord, id });
        axios.put("https://localhost:44311/api/datatable", { ...editRecord, id })
          .then(() => {
            this.tableData[key].surname = editRecord.surname;
            this.tableData[key].firstname = editRecord.firstname;
            this.tableData[key].patronymic = editRecord.patronymic;
            this.tableData[key].age = editRecord.age;
          })
          .catch(error => {
            //this.$refs.editRecordModal.errorMessage = error.message;
            alert("Sorry! " + error.message);
            console.error("Put request failed!", error);
          });
      },
      showAddRecordModal() {
        this.$refs.addRecordModal.show = true;
      },
      showEditRecordModal() {
        this.$refs.editRecordModal.show = true;
      }
    },
  };
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
  .box div {
    display: flex;
    width: 100%;
    flex-wrap: wrap;
  }

  .box-text {
    display: inline;
  }

  .box-button {
    margin: 4px 4px 5px 5px;
    margin-left:auto;
    display: inline;
  }

  .cw-table-list {
    table-layout: fixed;
    width: 100%;
  }

  tr th:first-child {
    width: 50px;
  }

  td {
    word-wrap: break-word;
  }

  .col-edit {
    word-wrap: initial;
    min-width: 100px;
  }
</style>
