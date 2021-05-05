<template>
  <div v-if="show" class="modal-shadow" @click.self="closeModal">
    <div class="modal">
      <div class="modal-close" @click="closeModal">&#10006;</div>
      <h3 class="modal-title">
        <slot name="title">
          Форма
        </slot>
      </h3>
      <form>
        <div class="form-group">
          <label for="surname">Фамилия</label>
          <input type="text"
                 placeholder="Ex: Иванов"
                 v-model="surname"
                 class="form-control" />
        </div>
        <div class="form-group">
          <label for="firstname">Имя</label>
          <input type="text"
                 placeholder="Ex: Иван"
                 v-model="firstname"
                 class="form-control" />
        </div>
        <div class="form-group">
          <label for="patronymic">Отчество</label>
          <input type="text"
                 placeholder="Ex: Иванович"
                 v-model="patronymic"
                 class="form-control" />
        </div>
        <div class="form-group">
          <label for="age">Возраст</label>
          <input type="number"
                 placeholder="Ex: 21"
                 v-model="age"
                 class="form-control" />
        </div>
        <button type="button" @click="onSave" class="btn btn-primary">
          <slot name="positive-button">
            Принять
          </slot>
        </button>
        <button type="button" @click="closeModal" class="btn btn-secondary">
          Отменить
        </button>
      </form>
    </div>
  </div>
</template>

<script>
  export default {
    name: "modal-window",
    data: () => ({
      show: false,
      surname: "",
      firstname: "",
      patronymic: "",
      age: "",
      key: ""
    }),
    methods: {
      clearForm() {
        this.surname = "";
        this.firstname = "";
        this.patronymic = "";
        this.age = "";
        this.key = "";
      },
      closeModal() {
        this.clearForm();
        this.show = false;
      },
      onSave() {
        this.$emit('positiveAction', {
          surname: this.surname,
          firstname: this.firstname,
          patronymic: this.patronymic,
          age: this.age,
        }, this.key);
        this.closeModal();
      }
    }
  }
</script>

<style scoped>
  .modal-shadow {
    position: fixed;
    top: 0;
    left: 0;
    min-height: 100%;
    width: 100%;
    background: rgba(0, 0, 0, 0.39);
    display: flex;
  }

  .modal {
    background: #fff;
    border-radius: 8px;
    padding: 15px;
    min-width: 420px;
    max-width: 480px;
    max-height: 460px;
    position: absolute;
    top: 50%;
    left: 50%;
    box-shadow: 2px 2px 20px 1px;
    overflow-x: auto;
    display: flex;
    flex-direction: column;
    transform: translate(-50%, -50%);
  }

  .modal-close {
    border-radius: 50%;
    color: #fff;
    background: #2a4cc7;
    display: flex;
    align-items: center;
    justify-content: center;
    position: absolute;
    top: 7px;
    right: 7px;
    width: 30px;
    height: 30px;
    cursor: pointer;
  }

  .modal-title {
    color: #0971c7;
  }
</style>
