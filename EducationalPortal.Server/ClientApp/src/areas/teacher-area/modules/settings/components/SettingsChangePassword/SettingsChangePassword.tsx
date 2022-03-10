import React, {FC} from 'react';
import {Form, Input, message} from 'antd';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {ButtonUpdate} from '../../../../../../components/ButtonUpdate/ButtonUpdate';
import {useNavigate} from 'react-router-dom';
import {useMutation} from '@apollo/client';

type FormValues = {
    oldPassword: string,
    newPassword: string,
}

export const SettingsChangePassword: FC = () => {
    const [form] = Form.useForm();
    const navigate = useNavigate();
    // const [updateStudent, updateStudentOptions] = useMutation<UpdateUserData, UpdateUserVars>(UPDATE_USER_MUTATION);

    const onFinish = async (values: FormValues) => {
        // updateStudent({
        //     variables: {
        //         updateUserInputType: {
        //         },
        //     },
        // })
        //     .then(response => {
        //         navigate('../');
        //     })
        //     .catch(error => {
        //         message.error(error.message);
        //     });
    };

    return (
        <Form
            name="TeachersUpdateForm"
            onFinish={onFinish}
            form={form}
            {...sizeFormItem}
        >
            <Form.Item
                name="oldPassword"
                label="Старий пароль"
                rules={[{required: true, message: 'Введіть Старий пароль!'}]}
            >
                <Input placeholder="Старий пароль"/>
            </Form.Item>
            <Form.Item
                name="newPassword"
                label="Новий пароль"
                rules={[{required: true, message: 'Введіть Новий пароль!'}]}
            >
                <Input placeholder="Новий пароль"/>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                {/*<ButtonUpdate loading={updateStudentOptions.loading} isSubmit={true}/>*/}
            </Form.Item>
        </Form>
    );
};
