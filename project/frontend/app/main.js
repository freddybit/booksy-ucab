import { createApp } from 'vue';
import { onMounted } from 'vue';
import App from './App.vue';
import router from "./router/index.js";
import 'vuetify/lib/components/VChip/VChip.css'
import 'vuetify/lib/components/VChipGroup/VChipGroup.css'
import 'vuetify/lib/components/VCombobox/VCombobox.css'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import {createVuetify} from 'vuetify';
import { createClient } from '@supabase/supabase-js'
import '../assets/styles/notifications.css';

const supabaseUrl = import.meta.env.VITE_SUPABASE_URL
const supabaseAnonKey = import.meta.env.VITE_SUPABASE_ANON_KEY

export const supabase = createClient(supabaseUrl, supabaseAnonKey)

const vuetify = createVuetify({
    components,
    directives,
})
const app = createApp(App);
app.use(router);
app.use(vuetify)
app.mount('#app');