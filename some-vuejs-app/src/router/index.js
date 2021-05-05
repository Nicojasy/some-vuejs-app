import Vue from 'vue'
import Router from 'vue-router'
import EditTable from '@/components/EditTable'

Vue.use(Router)

export default new Router({
  mode: 'history',
  routes: [
    {
      path: '/edit-table',
      name: 'EditTable',
      component: EditTable
    }
  ]
})
