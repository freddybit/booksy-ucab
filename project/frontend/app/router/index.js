
import { createRouter, createWebHistory } from 'vue-router';
import HomeView from '../views/Libro/HomeView.vue';
import BooksView from '../views/Libro/BooksView.vue';
import CatalogView from "../views/Libro/CatalogView.vue";
import BookDetailView from "../views/Libro/BookDetailView.vue";
import RegisterBookView from "../views/Libro/RegisterBookView.vue";
import LoginBuyerView from "../views/Perfil/perfil-comprador/LoginBuyerView.vue";
import LoginView from "../views/Perfil/perfil-vendedor/LoginView.vue";
import GestionarVentasView from "../views/Venta/GestionarVentasView.vue";
import RegisterProfileView from "../views/Perfil/perfil-vendedor/RegisterProfileView.vue";
import RegisterProfileBuyerView from "../views/Perfil/perfil-comprador/RegisterProfileBuyerView.vue";
import EditBookView from "../views/Libro/modificar-libro/EditBookView.vue";
import ProfileBuyerView from '@/views/Perfil/perfil-comprador/ProfileBuyerView.vue';
import SellerMenuView from '@/views/Perfil/perfil-vendedor/SellerMenuView.vue';

const routes = [
    { path: '/', name: 'Home', component: HomeView },
    { path: '/catalog', component: CatalogView },
    { path: '/books', component: BooksView },
    { path: '/book/:id', name: 'BookDetail', component: BookDetailView },
    { path: '/seller/registerBook', name: 'RegisterBook', component: RegisterBookView },
    { path: '/beginSection', component: LoginBuyerView },
    { path: '/registerBuyerSection', component: RegisterProfileBuyerView },
    { path: '/beginSellerSection', component: LoginView },
    { path: '/registerSellerSection', component: RegisterProfileView },
    { path: '/seller/sale', component: SellerMenuView},
    { path: '/seller/:email', name: 'Seller', component: SellerMenuView, props: true },
    { path: '/gestionarVenta/sale', component: GestionarVentasView },
    { path: '/seller/editBook', component: EditBookView, props: true },
    { path: '/buyer/myProfile/:email', name: 'MyProfile', component: ProfileBuyerView, props: true }
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;