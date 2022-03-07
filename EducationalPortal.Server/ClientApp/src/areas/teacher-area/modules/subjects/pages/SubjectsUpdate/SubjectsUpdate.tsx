import React from 'react';
import {useMutation, useQuery} from '@apollo/client';
import {Navigate, useNavigate, useParams} from 'react-router-dom';
import {Loading} from '../../../../../../components/Loading/Loading';
import {Form, Input, message} from 'antd';
import {ButtonUpdate} from '../../../../../../components/ButtonUpdate/ButtonUpdate';
import {sizeButtonItem, sizeFormItem} from '../../../../../../styles/form';
import {GET_SUBJECT_QUERY, GetSubjectData, GetSubjectVars} from '../../subjects.queries';
import {UPDATE_SUBJECT_MUTATION, UpdateSubjectData, UpdateSubjectVars} from '../../subjects.mutations';
import Title from 'antd/es/typography/Title';

type FormValues = {
    id: string,
    name: string,
    link: string,
}

export const SubjectsUpdate = () => {
    const params = useParams();
    const id = params.id as string;
    const [updateSubjectMutation, updateSubjectMutationOption] = useMutation<UpdateSubjectData, UpdateSubjectVars>(UPDATE_SUBJECT_MUTATION);
    const [form] = Form.useForm();
    const getSubjectQuery = useQuery<GetSubjectData, GetSubjectVars>(GET_SUBJECT_QUERY,
        {variables: {id: id}},
    );
    const navigate = useNavigate();


    const onFinish = async ({id, name, link}: FormValues) => {
        updateSubjectMutation({
            variables: {updateSubjectInputType: {id, name, link}},
        })
            .then(response => {
                navigate('../');
            })
            .catch(error => {
                message.error(error.message);
            });
    };

    if (!id)
        return <Navigate to={'/error'}/>;

    if (getSubjectQuery.loading)
        return <Loading/>;


    const subject = getSubjectQuery.data?.getSubject;
    return (
        <Form
            name="EducationalYearsUpdateForm"
            onFinish={onFinish}
            form={form}
            initialValues={{
                id: subject?.id,
                name: subject?.name,
                link: subject?.link,
            }}
            {...sizeFormItem}
        >
            <Title level={2}>Редагування предмету</Title>
            <Form.Item name="id" style={{display: 'none'}}>
                <Input type={'hidden'}/>
            </Form.Item>
            <Form.Item
                name="name"
                label="Назва"
                rules={[{required: true, message: 'Введіть назву!'}]}
            >
                <Input placeholder="Назва"/>
            </Form.Item>
            <Form.Item
                name="link"
                label="Посилання"
            >
                <Input placeholder="Посилання"/>
            </Form.Item>
            <Form.Item {...sizeButtonItem}>
                <ButtonUpdate loading={updateSubjectMutationOption.loading} isSubmit={true}/>
            </Form.Item>
        </Form>
    );
};
