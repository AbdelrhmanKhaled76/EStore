import { useEffect } from 'react';
import ErrorImage from '../../images/filtered products/error-image.jpg'
import { toast } from 'react-toastify';

const Error = () => {
    useEffect(()=>{
        toast.error('you are trying to access a non existing page or something went wrong please try again !!');
    })
    return (
        <div className="pt-48 pb-24 w-100 h-100 flex justify-center items-center">
            <img src={ErrorImage} alt="" className="w-1/2 h-1/2"/>
        </div>
    );
}

export default Error;
