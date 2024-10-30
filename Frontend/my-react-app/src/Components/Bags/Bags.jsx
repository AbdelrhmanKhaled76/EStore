import Slider from "react-slick";
import NextArrow from "../Arrows/NextArrow";
import PrevArrow from "../Arrows/PrevArrow";
import { Link } from "react-router-dom";
import axios from "axios";
import { useQuery } from "@tanstack/react-query";
import Loading from "../Loading/Loading";
import { url } from "../../App";
import pic1 from '/src/images/bags/last-sec-1.png'
import pic2 from '/src/images/bags/last-sec-2.png'
import Error from "../Error/Error";
import './Bags.css'



const Bags = () => {
  
  const  getBags =  ()=> axios.get(`${url}/api/products/Bags`,{
    headers : {
      'ngrok-skip-browser-warning' : 'asdasdas'
    }
  })

  const { data : bagsData, isLoading : bagsLoading, isError : bagsError } = useQuery({
    queryKey: ['getBags'],
    queryFn: getBags,
  });
  
  const  getAccessories =  ()=> axios.get(`${url}/api/products/accessories`,{
    headers : {
      'ngrok-skip-browser-warning' : 'asdasdas'
    }
  })

  const { data : accessoriesData, isLoading : accessoriesLoading, isError : accessoriesError } = useQuery({
    queryKey: ['getAccessories'],
    queryFn: getAccessories,
  });
  
  if(bagsLoading){
    return <Loading />
  }
  if(bagsError){
    return <Error />
  }


  if(accessoriesLoading){
    return <Loading />
  }
  if(accessoriesError){
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
      <div className="first-section-bags bg-cover bg-no-repeat"></div>
      <div className="container mx-auto py-20">
        <h1 className='h1 text-[--primary-color] text-center py-10'>bags</h1>
        <Slider {...Settings}>
          {bagsData?.data.map((product) => (
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
        <div className="second-section-bags  py-40  bg-no-repeat bg-cover bg-fixed bg-top">
        </div>
        <h2 className="h1 py-20 text-[--primary-color] text-center">accessories</h2>
        <div className="py-10 container mx-auto">
        <Slider {...Settings}>
          {accessoriesData?.data.map((product) => (
            <div key={product.id} className="layer-parent">
              <img src={product.url} alt={`Product ${product.id}`} className="w-full h-[258px] transition-all duration-500" />
              <div className="layer">
                <Link className="h2" to={'/product/'+product.id}>view product</Link>
                <p className="h4">price : {product.price} EGP</p>
              </div>
            </div>
          ))}
        </Slider>
        <div className="py-28 grid md:grid-cols-2 gap-20">
            <div className="w-full">
              <img src={pic1} alt="" className="w-full" />
            </div>
            <div className="w-full">
              <img src={pic2} alt="" className="w-full" />
            </div>
        </div>
        </div>
      </div>
  );
}

export default Bags;
