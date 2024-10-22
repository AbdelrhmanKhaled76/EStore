import { createBrowserRouter, RouterProvider } from "react-router-dom"
import Layout from "./Components/Layout/Layout"
import Home from "./Components/Home/Home"
import About from "./Components/About/About"
import Bags from "./Components/Bags/Bags"
import Wallets from "./Components/Wallets/Wallets"
import Cart from "./Components/Cart/Cart"
import Error from "./Components/Error/Error"
import Product from "./Components/Product/Product"
import { ToastContainer } from "react-toastify"
import { QueryClient, QueryClientProvider } from "@tanstack/react-query"
import Add from "./Components/Admin/Add"
import CartProvider from "./Contexts/CartProvider"
import Purchase from "./Components/Purchase/Purchase"

const queryClient = new QueryClient();
export const url = 'https://candy-shot-informal-jewellery.trycloudflare.com';


function App() {

  const router = createBrowserRouter([
    { 
      index : true , 
      element: <Layout>
      <Home/>
    </Layout>
    },
    {
      path : "/about", 
      element : <Layout>
        <About />
      </Layout>
    },
    {
      path : "/bags", 
      element : 
    <Layout>
      <Bags />
    </Layout>
    },
    {
      path : "/wallets", 
      element : <Layout>
        <Wallets />
      </Layout>
    },
    {
      path : "/cart", 
      element : <Layout>
        <Cart />
      </Layout>
    },
    {
      path : "/product/:Id", 
      element : <Layout>
        <Product />
      </Layout>
    },
    {
      path : "/admin", 
      element : <Layout>
        <Add />
      </Layout>
    },
    {
      path : "/purchase", 
      element : <Layout>
        <Purchase />
      </Layout>
    },
    {
      path : "*", 
      element : <Layout>
        <Error />
      </Layout>
    },
  ])

  return (
    <>
    <QueryClientProvider client={queryClient}>
      <ToastContainer autoClose={2000}/>
      <CartProvider>
        <RouterProvider router={router} />
      </CartProvider>
    </QueryClientProvider>
    </>
  )
}

export default App
