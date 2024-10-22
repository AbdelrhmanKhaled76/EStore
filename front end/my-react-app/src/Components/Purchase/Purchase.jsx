import axios from "axios";
import { useFormik } from "formik"
import * as Yup from 'yup';
import { url } from "../../App";
import { useContext } from "react";
import { CartContext } from "../../Contexts/CartProvider";
import { toast } from "react-toastify";

const Purchase = () => {

  const {cartProducts} = useContext(CartContext);

  const formik = useFormik({
    initialValues : {
      fname : '',
      lname : '',
      email : '',
      phone : '',
      address : '',
      payment : ''
    },

    validationSchema : Yup.object({
      fname : Yup.string().max(50,"can't be 50 characters or more").required('Required !!'),
      lname : Yup.string().max(50,"can't be 50 characters or more").required('Required !!'),
      email : Yup.string().email('not a valid email').required('Required !!'),
      phone : Yup.string().matches(/^01[0125]\d{8}$/, { 
        message : 'not a valid number',
        excludeEmptyString: true 
      }).required('Required !!'),
      address : Yup.string().required('Required !!'),
      payment : Yup.string()
    }),

    onSubmit : async values => {

      const cart = JSON.parse(JSON.stringify(cartProducts));


      const fromikValues = {
        'first_name' : values.fname,
        'last_name' : values.lname,
        'email' : values.email,
        'street' : values.address,
        'phone_number' : values.phone,
        'payment_method' : values.payment
      }

      const purchase = {
        'items' : cart,
        'billing_data' : fromikValues,
      }

      await axios.post(`${url}/api/payment/create-payment`, purchase)
      .then( res => {
        console.log('data is sent successfully ', res);
        const { link } = res.data;
        console.log(link);
        window.location.href = link;
      })
      .catch( err => console.log('an error has occured ',err) )
    }
  }) 

  return (
    <div className='pb-16 pt-3'>
        <div className="container mx-auto py-12">
          <form onSubmit={ formik.handleSubmit } className="grid grid-cols-1 md:grid-cols-2 gap-16 mx-auto">
            <div className="w-full">
              <label htmlFor="fname" className="block p">First Name</label>
              <input type="text" id="fname" placeholder="Ahmed...." className="form-input w-full" name="fname" {...formik.getFieldProps('fname')} />
              {formik.touched.fname && formik.errors.fname ? (
              <div className="mt-2 w-full text-center py-3 text-red-700">{formik.errors.fname}</div>
              ) : null}
            </div>
            <div className="w-full">
              <label htmlFor="lname" className="block p">Last Name</label>
              <input type="text" id="lname" placeholder="Mohamed...." className="form-input w-full" name="lname" {...formik.getFieldProps('lname')}/>
              {formik.touched.lname && formik.errors.lname ? (
              <div className="mt-2 w-full text-center py-3 text-red-700">{formik.errors.lname}</div>
              ) : null}
            </div>
            <div className="w-full">
              <label htmlFor="email" className="block p">Email</label>
              <input type="email" id="email" placeholder="Ahmed@gmail...." className="form-input w-full" name="email" {...formik.getFieldProps('email')}/>
              {formik.touched.email && formik.errors.email ? (
              <div className="mt-2 w-full text-center py-3 text-red-700">{formik.errors.email}</div>
              ) : null}
            </div>
            <div className="w-full">
              <label htmlFor="phone" className="block p">phone number</label>
              <input type="tel" id="phone" className="form-input w-full" placeholder="011..." name="phone" {...formik.getFieldProps('phone')}/>
              {formik.touched.phone && formik.errors.phone ? (
              <div className="mt-2 w-full text-center py-3 text-red-700">{formik.errors.phone}</div>
              ) : null}
            </div>
            <div className="w-full md:col-span-2">
              <label htmlFor="address" className="block p">address</label>
              <input type="text" id="address" placeholder="Your Address : " className="form-input w-full" name="address" {...formik.getFieldProps('address')}/>
              {formik.touched.address && formik.errors.address ? (
              <div className="mt-2 w-full text-center py-3 text-red-700">{formik.errors.address}</div>
              ) : null}
            </div>
            <div className="w-full md:col-span-2">
              <label htmlFor="payment" className="block p">payment method</label>
              <select name="payment" id="payment" {...formik.getFieldProps('payment')}>
                <option value="cash">cash</option>
                <option value="card">card</option>
                <option value="wallet">wallet</option>
              </select>
            </div>
            <button type="submit" className="md:col-span-2 w-fit mx-auto border border-black rounded-full   px-7 py-2 p text-black hover:bg-black hover:border-white hover:text-[--teritary-color] transition-all duration-500" >SUBMIT</button>
          </form>
        </div>
    </div>
  )
}

export default Purchase
