
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
import EditBookView from "../views/Libro/EditBookView.vue";
import ProfileBuyerView from '@/views/Perfil/perfil-comprador/ProfileBuyerView.vue';
import SellerMenuView from '@/views/Perfil/perfil-vendedor/SellerMenuView.vue';
import { components } from 'vuetify/dist/vuetify.js';
import SellerCatalog from '@/views/Perfil/perfil-vendedor/SellerCatalog.vue';
import SellerBookDetailView from '@/views/Perfil/perfil-vendedor/SellerBookDetailView.vue';
import pagar from "@/views/compra/pagar.vue";
import consultarPagos from "@/views/compra/consultarPagos.vue";
import eliminarPago from "@/views/compra/eliminarPago.vue"
import CategoryListView from '@/views/Libro/CategoryListView.vue';
import CategoryCatalogView from '@/views/Libro/CategoryCatalogView.vue';
import MyInformationView from '@/views/Perfil/perfil-comprador/MyInformationView.vue';
import DataAccountView from '@/views/Perfil/perfil-comprador/DataAccountView.vue';
import SecurityProfileView from '@/views/Perfil/perfil-comprador/SecurityProfileView.vue';

const routes = [
    { path: '/', name: 'Home', component: HomeView },
    { path: '/catalog', component: CatalogView },
    { path: '/categoryCatalog/:category', name: 'CategoryCatalog', component: CategoryCatalogView, props: true },
    { path: '/books', component: BooksView },
    { path: '/book/:id', name: 'BookDetail', component: BookDetailView },
    { path: '/sellerCatalog/book/:id', name: 'SellerBookDetail', component: SellerBookDetailView },
    { path: '/seller/registerBook/:email', name: 'RegisterBook', component: RegisterBookView, props: true },
    { path: '/beginSection', component: LoginBuyerView },
    { path: '/registerBuyerSection', component: RegisterProfileBuyerView },
    { path: '/beginSellerSection', component: LoginView },
    { path: '/registerSellerSection', component: RegisterProfileView },
    { path: '/seller/sale', component: SellerMenuView},
    { path: '/seller/:email', name: 'Seller', component: SellerMenuView, props: true },
    { path: '/gestionarVenta/sale/:email', name: 'GestionarVentas', component: GestionarVentasView, props: true },
    { path: '/seller/editBook/:id', name:'EditBook' , component: EditBookView, props: true },
    { path: '/buyer/myProfile/:email', name: 'MyProfile', component: ProfileBuyerView, props: true },
    { path: '/seller/sellerCatalog/:email', name: 'SellerCatalog', component: SellerCatalog, props: true },
    { path: '/pagar/:id', name: 'pagar', component: pagar, props: true},
    { path: '/consultarPagos', name: 'compras', component: consultarPagos},
    { path: '/eliminarPago/:id', name: 'eliminarCompra', component: eliminarPago, props: true},
    { path: '/categoryList', component: CategoryListView, name: 'CategoryList' },
    { path: '/buyer/myProfile/information/:email', name: 'MyInformation', component: MyInformationView, props: true },
    { path: '/buyer/myProfile/data/:email', name: 'DataAccount', component: DataAccountView, props: true },
    { path: '/buyer/myProfile/security/:email', name: 'SecurityProfile', component: SecurityProfileView, props: true },
];

const router = createRouter({
    history: createWebHistory(),
    routes
});

export default router;