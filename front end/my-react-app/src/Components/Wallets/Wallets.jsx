import Slider from "react-slick";
import NextArrow from '../Arrows/NextArrow';
import PrevArrow from '../Arrows/PrevArrow';
import { Link } from "react-router-dom";
import Loading from "../Loading/Loading";
import axios from "axios";
import { useQuery } from "@tanstack/react-query";
import { url } from "../../App";
import pic1 from '/src/images/wallets/last-sec-1.png'
import pic2 from '/src/images/wallets/last-sec-2.png'
import Error from "../Error/Error";
import './Wallets.css'


const Wallets = () => {

  const  getwalletMen =  ()=> axios.get(`${url}/api/products/menwallets`,{
    headers : {
      'ngrok-skip-browser-warning' : 'asdasdas'
    }
  })

  const { data : walletMenData, isLoading : walletMenLoading, isError : walletMenError } = useQuery({
    queryKey: ['getwalletMen'],
    queryFn: getwalletMen,
  });
  
  const  getwalletWomen =  ()=> axios.get(`${url}/api/products/womenwallets`,{
    headers : {
      'ngrok-skip-browser-warning' : 'asdasdas'
    }
  })

  const { data : walletWomenData, isLoading : walletWomenLoading, isError : walletWomenError } = useQuery({
    queryKey: ['getwalletWomen'],
    queryFn: getwalletWomen,
  });
  
  if(walletMenLoading){
    return <Loading />
  }
  if(walletMenError){
    return <Error />;
  }


  if(walletWomenLoading){
    return <Loading />
  }
  if(walletWomenError){
    return <Error />;
  }

    const Settings = {
        infinite: true,
        rows: 1,
        centerMode: true,
        centerPadding: "60px",
        speed: 500,
        slidesToShow: 4,
        arrows: true,
        prevArrow : <PrevArrow/>,
        nextArrow : <NextArrow/>,
        responsive: [
          {
            breakpoint: 1536,
            settings: {
              slidesToShow: 3,
            }
          },
          {
            breakpoint: 1280,
            settings: {
              slidesToShow: 2,
            }
          },
          {
            breakpoint: 768,
            settings: {
              slidesToShow: 1,
            }
          },
          {
            breakpoint: 640,
            settings: {
              slidesToShow: 1,
              centerMode : true,
              centerPadding : 0,
            }
          }
        ]
    };
    return (
        <div>
        <div className="first-section-wallets bg-cover bg-no-repeat"></div>
        <div className="container mx-auto py-20">
        <h1 className='h1 text-[--primary-color] text-center py-10'>men wallet</h1>
        <Slider {...Settings}>
          {walletMenData?.data.map((product) => (
            <div key={product.id} className="layer-parent">
              <img src={product.url} alt={`Product ${product.id}`} className="w-full h-[258px] transition-all duration-500" />
              <div className="layer">
                <Link className="h2" to={'/product/'+product.id}>view product</Link>
                <p className="h4">price : {product.price} EGP</p>
              </div>
            </div>
          ))}
        </Slider>
        </div>
        <div className="bg-between-two-section">
        </div>
        <div className="container mx-auto">
        <h2 className="h1 py-10 text-[--primary-color] text-center">women wallet</h2>
        <div className="py-10">
        <Slider {...Settings}>
          {walletWomenData?.data.map((product) => (
            <div key={product.id} className="layer-parent">
              <img src={product.url} alt={`Product ${product.id}`} className="w-full h-[258px] transition-all duration-500" />
              <div className="layer">
                <Link className="h2" to={'/product/'+product.id}>view product</Link>
                <p className="h4">price : {product.price} EGP</p>
              </div>
            </div>
          ))}
        </Slider>
        </div>
        <div className="py-28 grid md:grid-cols-3 gap-20">
            <div className="w-full">
              <img src={pic1} alt="collection photo 1" className="w-full h-full" />
            </div>
            <div className="md:col-span-2 w-full">
              <img src={pic2} alt="collection photo 2" className="w-full h-full" />
            </div>
        </div>
        </div>
        
        
    </div>
    );
}

export default Wallets;
